using System.Collections.Generic;
using Architecture;
using Command.Battle;
using QFramework;
using UnityEngine;

namespace Controller.Particle_Effect
{
    public class RockShootController : ParticleEffectController
    {
        private const AttackEffectType Type = AttackEffectType.KnockUp;

        private Transform mOwner;
        private List<ParticleCollisionEvent> mCollisionEvents = new List<ParticleCollisionEvent>();

        protected override void Awake()
        {
            particles = GetComponentsInChildren<ParticleSystem>();

            var main = particles[0].main;
            main.stopAction = ParticleSystemStopAction.Callback;
            // 单独写出，如果更改子发射器组件的 stopAction 会 warning
            
            foreach (var particle in particles)
            {
                main = particle.main;
                main.loop = false;
                particle.Play();
            }
        }
        
        private void OnParticleCollision(GameObject other)
        {
            particles[0].GetCollisionEvents(other, mCollisionEvents);
            this.SendCommand(new TryToHurtPlayerCommand(Type, mOwner, mCollisionEvents[0].intersection));
        }

        public RockShootController SetOwner(Transform enemy)
        {
            mOwner = enemy;
            return this;
        }
    }
}
