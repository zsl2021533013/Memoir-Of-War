using Architecture;
using Event.Character;
using Event.Character.Player;
using Event.Statue;
using Extension;
using Model.Particle_Effect;
using Model.Player;
using QFramework;
using UnityEngine;

namespace System.Particle_System
{
    public interface IParticleEffectSystem : ISystem
    {
        
    }
    
    public class ParticleEffectSystem : AbstractSystem, IParticleEffectSystem
    {
        protected override void OnInit()
        {
            this.RegisterEvent<PlayerDefenceSuccessEvent>(e =>
            {
                var effect = this.GetModel<IParticleEffectModel>()
                    .PoolDic
                    .GetValue(ParticleType.Defence)
                    .Allocate()
                    .Position(e.pos);
                effect.SetActive(true);
            });

            this.RegisterEvent<PlayerDefenceStatueEvent>(e =>
            {
                var player = this.GetModel<IPlayerModel>().Transform;
                var effect = this.GetModel<IParticleEffectModel>()
                    .PoolDic
                    .GetValue(ParticleType.Defence)
                    .Allocate()
                    .Position(player.position + 2 * Vector3.up);
                effect.SetActive(true);
            });
        }
    }
}
