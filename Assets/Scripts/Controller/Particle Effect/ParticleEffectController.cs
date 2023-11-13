using System.Collections.Generic;
using Architecture;
using Command.Particle_Effect;
using QFramework;
using UnityEngine;

namespace Controller.Particle_Effect
{
    public class ParticleEffectController : MonoBehaviour, IController
    {
        protected ParticleSystem[] particles;

        protected virtual void OnEnable()
        {
            foreach (var particle in particles)
            {
                particle.Play();
            }
        }

        protected virtual void Awake()
        {
            particles = GetComponentsInChildren<ParticleSystem>();

            foreach (var particle in particles)
            {
                var main = particle.main;
                main.loop = false;
                main.stopAction = ParticleSystemStopAction.Callback;
                particle.Play();
            }
        }

        private void OnParticleSystemStopped()
        {
            this.SendCommand(new ParticleSystemStoppedCommand(transform.parent.gameObject));
        }

        public void SetPlaySpeed(float num)
        {
            foreach (var particle in particles)
            {
                var main = particle.main;
                main.simulationSpeed = num;
            }
        }
        
        public void ResetPlaySpeed()
        {
            foreach (var particle in particles)
            {
                var main = particle.main;
                main.simulationSpeed = 1f;
            }
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}