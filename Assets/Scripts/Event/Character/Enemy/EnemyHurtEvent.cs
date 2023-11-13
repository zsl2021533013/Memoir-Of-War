using Architecture;
using UnityEngine;

namespace Event.Character.Enemy
{
    public class EnemyHurtEvent
    {
        public Transform enemyTransform;
        public AttackEffectType type;

        public EnemyHurtEvent(Transform enemyTransform, AttackEffectType type)
        {
            this.enemyTransform = enemyTransform;
            this.type = type;
        }
    }
}