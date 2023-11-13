using Architecture;
using Extension;
using Model.Particle_Effect;
using QFramework;
using UnityEngine;

namespace Command.Particle_Effect
{
    public class HitSpawnCommand : AbstractCommand
    {
        public Vector3 pos;

        public HitSpawnCommand(Vector3 pos)
        {
            this.pos = pos;
        }
        
        protected override void OnExecute()
        {
            this.GetModel<IParticleEffectModel>()
                .PoolDic
                .GetValue(ParticleType.Hit)
                .Allocate()
                .Position(pos)
                .SetActive(true);
        }
    }
}