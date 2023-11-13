using Architecture;
using Extension;
using Model.Particle_Effect;
using Model.Player;
using QFramework;

namespace Command.Particle_Effect
{
    public class BeamSpawnCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var player = this.GetModel<IPlayerModel>().Transform;
            
            var pool = this.GetModel<IParticleEffectModel>().PoolDic.GetValue(ParticleType.Beam);
            var effect = pool.Allocate();

            effect.transform.Position(player.position);
            effect.SetActive(true);
        }
    }
}