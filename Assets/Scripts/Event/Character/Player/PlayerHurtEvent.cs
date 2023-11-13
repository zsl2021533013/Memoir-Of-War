using Architecture;
using UnityEngine;

namespace Event.Character.Player
{
    public class PlayerHurtEvent
    {
        public Transform enemyTransform;
        public AttackEffectType type;

        public PlayerHurtEvent(Transform enemyTransform, AttackEffectType type)
        {
            this.enemyTransform = enemyTransform;
            this.type = type;
        }
    }
}