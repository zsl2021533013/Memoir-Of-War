using UnityEngine;

namespace Event.Character.Enemy
{
    public class EnemyParriedEvent
    {
        public Transform Enemy { get; private set; }

        public EnemyParriedEvent(Transform enemy)
        {
            Enemy = enemy;
        }
    }
}