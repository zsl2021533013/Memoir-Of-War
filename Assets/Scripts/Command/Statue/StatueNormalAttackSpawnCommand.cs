using Architecture;
using Extension;
using Model.Particle_Effect;
using Model.Player;
using QFramework;

namespace Command.Statue
{
    public class StatueNormalAttackSpawnCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var pool = this.GetModel<IParticleEffectModel>().PoolDic.GetValue(ParticleType.StatueNormalAttack);
            var effect = pool.Allocate();
            
            effect.transform.Position(this.GetModel<IPlayerModel>().Transform.position);
            effect.SetActive(true);
        }
    }
}