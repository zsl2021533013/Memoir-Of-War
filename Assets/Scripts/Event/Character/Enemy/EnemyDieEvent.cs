using UnityEngine;

namespace Event.Character.Enemy
{
    public class EnemyDieEvent
    {
        public EnemyDieEvent(Transform enemy)
        {
            Enemy = enemy;
        }
        
        public Transform Enemy { get; private set; }
    }
}