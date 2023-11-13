using Architecture;
using Command.Portal;
using Command.Timeline;
using DG.Tweening;
using Extension;
using QFramework;
using QFramework.Example;
using Scriptable_Object;
using UnityEngine;

namespace Controller.Portal
{
    public class PortalController : MonoBehaviour, IController
    {
        public PortalDestinationController destination;
        public SceneNameScriptableObject destinationName;

        private Vector3 detectSize = new Vector3(5f, 5f, 0.1f);

        private void Start()
        {
            if (destination == null)
            {
                Debug.LogError("Portal Destination Missing");
            }
            
            destination.Init(this);
        }

        private void Update()
        {
            destination.UpdatePortalCamera();
        }

        private void FixedUpdate()
        {
            if (DetectPlayer())
            {
                this.SendCommand(new PlayerTeleportCommand(transform, destination.transform));
            
                this.SendCommand<PlayerStopMoveForwardCommand>();

                if (destinationName.mainTitle == "圣灵殿") // 回到了主页面
                {
                    DOVirtual.DelayedCall(MemoirOfWarAsset.SceneNameMainMenuDelay, () =>
                    {
                        UIKit.OpenPanel<UISceneNamePanel>(new UISceneNamePanelData()
                        {
                            mainTitle = destinationName.mainTitle,
                            subTitle = destinationName.subTitle
                        });
                    });
                    
                    AudioKit.PlayMusicWithTransition(
                        BGMType.MainTheme.ToString().InsertSpace(),
                        MemoirOfWarAsset.BGMTranslationDuration
                    );
                }
                else
                {
                    DOVirtual.DelayedCall(MemoirOfWarAsset.SceneNameDelay, () =>
                    {
                        UIKit.OpenPanel<UISceneNamePanel>(new UISceneNamePanelData()
                        {
                            mainTitle = destinationName.mainTitle,
                            subTitle = destinationName.subTitle
                        });
                    });
                    
                    AudioKit.PlayMusicWithTransition(
                        BGMType.SubTheme.ToString().InsertSpace(),
                        MemoirOfWarAsset.BGMTranslationDuration,
                        MemoirOfWarAsset.BGMSubThemeVolume
                    );
                }
                
            }
        }

        public Material GetPortalMaterial()
            => transform.GetComponentInChildren<MeshRenderer>().material;
        
        private bool DetectPlayer() => 
            Physics.CheckBox(transform.position, detectSize, transform.rotation, MemoirOfWarAsset.PlayerLayerMask);
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, 2 * detectSize);
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}
