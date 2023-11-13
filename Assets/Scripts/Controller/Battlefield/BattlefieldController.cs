using System;
using System.Collections.Generic;
using System.Input_System;
using System.Linq;
using Architecture;
using Command.Battle;
using DG.Tweening;
using Event.Character.Enemy;
using Extension;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Controller.Battlefield
{
    [Serializable]
    public class SpawnPoint
    {
        public EnemyType type;
        public Transform point;
    }
    
    public class BattlefieldController : MonoBehaviour, IController
    {
        private const string ProtagonistPosition = "_ProtagonistPosition";
        
        public List<SpawnPoint> spawnPoints;
        
        private GameObject mAirWall;

        private List<Material> mAirWallMats = new List<Material>();

        private Transform mPlayerTrans;
        
        private bool mIsUsed = false;
        
        private bool mIsActive = false;
        
        private int mActiveEnemyNum;
        
        private ResLoader mLoader = ResLoader.Allocate();

        private void Awake()
        {
            mAirWall = transform.FindAllChildren("Air Wall").gameObject;

            var airWallMat = mLoader.LoadSync<Material>("Battlefield Air Wall Mat");
            var renderers = mAirWall.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                renderer.transform.Layer(MemoirOfWarAsset.AirWallLayer);
                renderer.material = airWallMat;
                mAirWallMats.AddRange(renderer.materials);
            }
            
            foreach (var unused in spawnPoints.Where(spawnPoint => spawnPoint.point == null))
            {
                Debug.LogError("Spawn Point Missing");
            }
        }

        private void Start()
        {
            mAirWall.SetActive(false);
        }

        private void Update()
        {
            if (!mIsActive)
            {
                return;
            }

            foreach (var material in mAirWallMats)
            {
                material.SetVector(ProtagonistPosition, mPlayerTrans.position);
            }
        }

        private void OpenBattlefield()
        {
            Debug.Log("Battlefield Open");
            
            this.RegisterEvent<EnemyDieEvent>(OnEnemyDie);
            
            mActiveEnemyNum = 0;

            mIsUsed = true; // 只能开启一次

            mIsActive = true;

            mAirWall.SetActive(true); // 开启空气墙

            mPlayerTrans = this.GetModel<IPlayerModel>().Transform;
        }
        
        private void CloseBattlefield()
        {
            this.UnRegisterEvent<EnemyDieEvent>(OnEnemyDie);

            mIsActive = false;
            
            mAirWall.SetActive(false); // 开启空气墙
        }

        private void OnEnemyDie(EnemyDieEvent e)
        {
            mActiveEnemyNum--;

            if (mActiveEnemyNum == 0)
            {
                CloseBattlefield();
            }
        }

        public void SpawnEnemy()
        {
            if (mIsUsed)
            {
                return;
            }
            
            OpenBattlefield();

            DOTween.Sequence()
                .AppendCallback(ConstrainPlayer)
                .AppendInterval(1f)
                .AppendCallback(ResetPlayer);

            foreach (var spawnPoint in spawnPoints)
            {
                var playerTrans = this.GetModel<IPlayerModel>().Transform;
                var rotation = Quaternion
                    .LookRotation(playerTrans.position - spawnPoint.point.position).eulerAngles;
                rotation = new Vector3(0f, rotation.y, 0f);

                this.SendCommand(new EnemySpawnCommand(spawnPoint.type, spawnPoint.point.position, 
                    Quaternion.Euler(rotation)));

                mActiveEnemyNum++;
            }
        }

        private void ConstrainPlayer()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            
            playerModel.Controller.DisableRootMotion();
            
            playerModel.Rigidbody.velocity = Vector3.zero; // 速度必须清零！不然残余速度会使玩家滑行

            InputKit.Instance.DisableMovement();
        }

        private void ResetPlayer()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            
            playerModel.Controller.EnableRootMotion();
            
            InputKit.Instance.EnableMovement();
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}