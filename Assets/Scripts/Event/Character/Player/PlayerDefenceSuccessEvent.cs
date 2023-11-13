using Architecture;
using UnityEngine;

namespace Event.Character.Player
{
    public class PlayerDefenceSuccessEvent
    {
        public AttackEffectType type;
        public Vector3 pos;

        public PlayerDefenceSuccessEvent(AttackEffectType type, Vector3 pos)
        {
            this.type = type;
            this.pos = pos;
        }
    }
}