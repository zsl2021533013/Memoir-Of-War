using Architecture;
using Command.Particle_Effect;
using Event.Character.Enemy;
using Event.Character.Player;
using QFramework;
using UnityEngine;

namespace Command.Battle
{
    public class TryToHurtEnemyCommand : AbstractCommand
    {
        private Transform mEnemy;
        private Vector3 mHitPos;
        private AttackEffectType mType;

        public TryToHurtEnemyCommand(Transform enemy, Vector3 hitPos, AttackEffectType type = AttackEffectType.Normal)
        {
            mEnemy = enemy;
            mHitPos = hitPos;
            mType = type;
        }
        
        protected override void OnExecute()
        {
            this.SendCommand(new HitSpawnCommand(mHitPos));
            
            this.SendEvent(new EnemyHurtEvent(mEnemy, mType));
            
            this.SendEvent<HitBulletTimeStartEvent>();
        }
    }
}