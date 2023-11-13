using UnityEngine;

namespace Event.Character.Player
{
    public class PlayerKnockBackEvent
    {
        public Transform EnemyTransform;
        public float Velocity;

        public PlayerKnockBackEvent(Transform transform, float velocity)
        {
            
        }
    }
}