using System.Collections.Generic;
using System.Input_System;
using Architecture;
using Cinemachine;
using DG.Tweening;
using Extension;
using Model.Player;
using QFramework;
using QFramework.Example;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Controller.Timeline
{
    public struct GameStartTimeline {}
    
    public struct Return2MainMenuTimeline {}

    public class DirectorController : MonoSingleton<DirectorController>, IController
    {
        private ResLoader mLoader = ResLoader.Allocate();
    
        private PlayableDirector mDirector;
    
        private Dictionary<string, TrackAsset> mTrackDic = new ();
        private Dictionary<string, PlayableBinding> mBingingDic = new ();

        public override void OnSingletonInit()
        {
            InitComponent();
            
            InitPlayableAsset();
        }

        private void InitComponent()
        {
            transform.name = "Director";
            
            mDirector = gameObject.AddComponent<PlayableDirector>();
            mDirector.playOnAwake = false; // 不能一开始就播放
            
            mDirector.played += director => InputKit.Instance.DisableInput();
            mDirector.played += director =>
            {
                var model = this.GetModel<IPlayerModel>();
                model.ThirdPersonCamera.ResetPriority();
                model.StatueCamera.ResetPriority();
                model.PortalCamera.ResetPriority();
                model.StartCamera.ResetPriority();
            }; // 重置相机，新的相机需要在 timeline 播放后指定
            mDirector.played += director => this.GetModel<IPlayerModel>().Controller.EnableTimeline();
            mDirector.stopped += director => InputKit.Instance.EnableInput();

            DontDestroyOnLoad(gameObject);
        }

        private void InitPlayableAsset()
        {
            TypeEventSystem.Global.Register<GameStartTimeline>(e =>
            {
                var model = this.GetModel<IPlayerModel>();
                Instance
                    .SetTrack("Cinemachine Track", model.CinemachineBrain)
                    .SetCinemachineClip("Cinemachine Track", "Start Camera", model.StartCamera)
                    .SetCinemachineClip("Cinemachine Track", "Portal Camera", model.PortalCamera)
                    .SetCinemachineClip("Cinemachine Track", "Third Person Camera", model.ThirdPersonCamera)
                    .Play();
                
                model.ThirdPersonCamera.IncreasePriority();
            });
            
            TypeEventSystem.Global.Register<Return2MainMenuTimeline>(e =>
            {
                var model = this.GetModel<IPlayerModel>();
                Instance
                    .SetTrack("Cinemachine Track", model.CinemachineBrain)
                    .SetCinemachineClip("Cinemachine Track", "Third Person Camera", model.ThirdPersonCamera)
                    .SetCinemachineClip("Cinemachine Track", "Portal Camera", model.PortalCamera)
                    .SetCinemachineClip("Cinemachine Track", "Start Camera", model.StartCamera)
                    .Play();
                
                model.StartCamera.IncreasePriority();

                mDirector.stopped += OpenUIHomePanelOnStopped;
            });
        }

        private DirectorController Play()
        {
            mDirector.Play();
            return this;
        }

        public DirectorController LoadAsset<T>() where T : new()
        {
            var assetName = typeof(T).Name;
            var asset = mLoader.LoadSync<PlayableAsset>(assetName);

            if (asset == null)
            {
                Debug.LogError("Playable Asset Missing");
                return this;
            }
        
            #region Update Dictionary

            mBingingDic.Clear();
            foreach (var bind in asset.outputs)
            {
                mBingingDic.TryAdd(bind.streamName, bind);
            }
        
            mTrackDic.Clear();
            foreach (var track in ((TimelineAsset)asset).GetOutputTracks())
            {
                mTrackDic.TryAdd(track.name, track);
            }

            #endregion
        
            mDirector.playableAsset = asset;

            TypeEventSystem.Global.Send<T>(); // 获取并触发 Asset 具体绑定方法
        
            return this;
        }

        private DirectorController SetTrack(string trackName, Object trackObject)
        {
            var bind = mBingingDic.GetValue(trackName);
            mDirector.SetGenericBinding(bind.sourceObject, trackObject);
            return this;
        }

        private DirectorController SetCinemachineClip(string trackName, string clipName, CinemachineVirtualCameraBase cameraBase)
        {
            var track = mTrackDic.GetValue(trackName);
        
            foreach (var info in track.GetClips())
            {
                if (info.displayName == clipName)
                {
                    var cameraInfo = (CinemachineShot)info.asset;
                    var cameraRef = new ExposedReference<CinemachineVirtualCameraBase>() { defaultValue = cameraBase };
                    cameraInfo.VirtualCamera = cameraRef;
                    break;
                }
            }
        
            return this;
        }

        private void OpenUIHomePanelOnStopped(PlayableDirector director)
        {
            InputKit.Instance.DisableInput();
            
            DOVirtual.DelayedCall(MemoirOfWarAsset.SceneNameShowTime, () =>
            {
                UIKit.OpenPanel<UIHomePanel>(new UIHomePanelData()
                {
                    StartCamera = GameObject.Find("Start Camera").GetComponent<CinemachineVirtualCameraBase>(),
                    Entrance = GameObject.Find("Entrance").transform
                });

                mDirector.stopped -= OpenUIHomePanelOnStopped; // 只触发一次，下次就不触发了
            }); // 等待地名显示完毕
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}