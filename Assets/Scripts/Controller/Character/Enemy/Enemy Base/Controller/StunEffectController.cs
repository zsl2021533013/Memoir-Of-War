using Architecture;
using DG.Tweening;
using QFramework;
using UnityEngine;

namespace Controller.Character.Enemy.Enemy_Base.Controller
{
    public class StunEffectController : MonoBehaviour, IController
    {
        private ParticleSystem[] mParticles;
        
        private void Awake()
        {
            mParticles = GetComponentsInChildren<ParticleSystem>();
        }
    
        public void Play()
        {
            foreach (var particle in mParticles)
            {
                particle.Play();
            }
        }
        
        public void Stop()
        {
            foreach (var particle in mParticles)
            {
                particle.Stop();
            }
        }
    
        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}