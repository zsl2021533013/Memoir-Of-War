using Architecture;
using QFramework;
using UnityEngine;

namespace Controller.Statue
{
    public class BlessController : MonoBehaviour, IController
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