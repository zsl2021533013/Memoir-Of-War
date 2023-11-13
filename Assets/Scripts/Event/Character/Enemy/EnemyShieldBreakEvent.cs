using UnityEngine;

namespace Event.Character.Enemy
{
    public class EnemyShieldBreakEvent
    {
        public Transform Enemy { get; private set; }
        
        public EnemyShieldBreakEvent(Transform enemy)
        {
            Enemy = enemy;
        }
    }
}