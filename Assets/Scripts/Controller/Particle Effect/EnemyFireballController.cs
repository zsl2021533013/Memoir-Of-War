using System;
using System.Collections.Generic;
using Architecture;
using Command.Battle;
using Command.Particle_Effect;
using Event;
using Event.Character.Player;
using QFramework;
using UnityEngine;

namespace Controller.Particle_Effect
{
    public class EnemyFireballController : ParticleEffectController
    {
        private const AttackEffectType Type = AttackEffectType.Fireball;

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

            this.RegisterEvent<FireballParriedEvent>(OnFireballParried); 
            // 玩家成功格挡则立即关闭，避免后续的爆炸特效污染画面
        }
        
        private void OnParticleCollision(GameObject other)
        {
            particles[0].GetCollisionEvents(other, mCollisionEvents);
            this.SendCommand(new TryToHurtPlayerCommand(Type, mOwner, mCollisionEvents[0].intersection));
        }

        private void OnDestroy()
        {
            this.UnRegisterEvent<FireballParriedEvent>(OnFireballParried);
        }

        public EnemyFireballController SetOwner(Transform enemy)
        {
            mOwner = enemy;
            return this;
        }

        private void OnFireballParried(FireballParriedEvent e)
        {
            if (e.owner == mOwner)
            {
                foreach (var particle in particles)
                {
                    particle.Stop();
                }
            }
        }
    }
}
