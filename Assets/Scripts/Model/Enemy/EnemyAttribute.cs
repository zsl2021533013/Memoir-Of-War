using System;
using QFramework;

namespace Model.Enemy
{
    public class EnemyAttribute
    {
        public EnemyAttribute(float health, float shield)
        {
            Health.Value = health;
            Shield.Value = shield;
        }

        public EnemyAttribute(EnemyAttribute templateAttribute)
        {
            Health.Value = templateAttribute.Health.Value;
            Shield.Value = templateAttribute.Shield.Value;
        }

        public BindableProperty<float> Health { get; set; } = new BindableProperty<float>();
        public BindableProperty<float> Shield { get; set; } = new BindableProperty<float>();
    }
}