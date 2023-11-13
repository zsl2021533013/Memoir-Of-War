using Architecture;
using Controller.Battlefield;
using Controller.Character.Enemy.Enemy_Base.Controller;
using UnityEngine;
using UnityEngine.AI;

namespace Model.Enemy
{
    public class EnemyData
    {
        public EnemyData(EnemyType type, Transform transform, EnemyAttribute enemyAttribute)
        {
            Type = type;
            Transform = transform;
            Collider = transform.GetComponentInChildren<Collider>();
            Animator = transform.GetComponentInChildren<Animator>();
            Agent = transform.GetComponentInChildren<NavMeshAgent>();
            Controller = transform.GetComponentInChildren<EnemyController>();
            Attribute = enemyAttribute;
        }

        public EnemyType Type { get; }
        public Transform Transform { get; }
        public Collider Collider { get; }
        public Animator Animator { get; }
        public NavMeshAgent Agent { get; }
        public EnemyController Controller { get; }
        public EnemyAttribute Attribute { get; private set; }
        
        public EnemyData DecreaseHealth(float num)
        {
            Attribute.Health.Value -= num;
            return this;
        }
        
        public EnemyData DecreaseShield(float num)
        {
            Attribute.Shield.Value -= num;
            return this;
        }

        public EnemyData ResetAttribute(EnemyAttribute attribute)
        {
            Attribute.Health.SetValueWithoutEvent(attribute.Health.Value);
            Attribute.Shield.SetValueWithoutEvent(attribute.Shield.Value);
            return this;
        }
        
        public EnemyData ResetShield(EnemyAttribute attribute)
        {
            Attribute.Shield.Value = attribute.Shield.Value;
            return this;
        }
    }
}