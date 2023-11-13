using Architecture;
using Event.Character;
using Event.Character.Player;
using Extension;
using Model.Enemy;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Command.Battle
{
    public class EnemyHurtCommand : AbstractCommand
    {
        private Transform mEnemy;
        private Vector3 mHitPos;
        private AttackEffectType mType;

        public EnemyHurtCommand(Transform enemy, Vector3 hitPos, AttackEffectType type = AttackEffectType.Normal)
        {
            mEnemy = enemy;
            mHitPos = hitPos;
            mType = type;
        }
        
        protected override void OnExecute()
        {
            var date = this.GetModel<IEnemyModel>().DataDic.GetValue(mEnemy);

            this.SendEvent(new HitEffectSpawnEvent(mHitPos));
            
            this.SendEvent<HitBulletTimeStartEvent>();
            
            
            if (mType == AttackEffectType.Fireball)
            {
                date.DecreaseHealth(15f);
                date.DecreaseShield(20f);
            }
            else
            {
                date.DecreaseHealth(5f);
            }
        }
    }
}