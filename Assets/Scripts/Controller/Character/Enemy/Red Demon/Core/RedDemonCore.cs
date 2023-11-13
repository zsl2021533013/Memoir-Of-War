using Architecture;
using Command.Particle_Effect;
using Controller.Character.Enemy.Enemy_Base.Core;
using DG.Tweening;
using Extension;
using Model.Particle_Effect;
using QFramework;
using UnityEngine;

namespace Controller.Character.Enemy.Red_Demon.Core
{
    public class RedDemonCore : EnemyCore
    {
        private const float DOTweenDuration = 0.5f;

        private const float Attack3Distance = 5f;
        public override float ArriveDistance => 4f;
        
        private Transform mWeapon;
        
        public override void InitCore(Transform rootTrans)
        {
            base.InitCore(rootTrans);
            
            mWeapon = rootTrans.FindAllChildren("Weapon");
        }

        public bool HasArrivedAttack4Range()
        {
            var remainingDistance = agent.pathPending ? float.PositiveInfinity : agent.remainingDistance;
            return ArriveDistance <= remainingDistance && remainingDistance <= ArriveDistance + Attack3Distance;
        }
        
        public void ExtendSword()
        {
            mWeapon.DOScale(new Vector3(1f, 2f, 1f), DOTweenDuration);
        }

        public void ContractSword()
        {
            mWeapon.DOScale(new Vector3(1f, 1f, 1f), DOTweenDuration);
        }

        public void RockShoot()
        {
            this.SendCommand(new RockShootSpawnCommand(enemyTrans));
        }
    }
}