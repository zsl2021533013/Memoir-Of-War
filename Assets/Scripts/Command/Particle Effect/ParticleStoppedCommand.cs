using System;
using Architecture;
using Extension;
using Model.Particle_Effect;
using QFramework;
using UnityEngine;

namespace Command.Particle_Effect
{
    public class ParticleSystemStoppedCommand : AbstractCommand
    {
        private GameObject mParticle;

        public ParticleSystemStoppedCommand(GameObject gameObject)
        {
            mParticle = gameObject;
        }
        
        protected override void OnExecute()
        {
            var type = (ParticleType)Enum.Parse(typeof(ParticleType), mParticle.name);
            this.GetModel<IParticleEffectModel>().PoolDic.GetValue(type).Recycle(mParticle);
        }
    }
}