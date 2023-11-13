using Architecture;
using Controller.Particle_Effect;
using Extension;
using Model.Particle_Effect;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Command.Particle_Effect
{
    public class RockShootSpawnCommand : AbstractCommand
    {
        private Transform mEnemy;

        public RockShootSpawnCommand(Transform transform)
        {
            mEnemy = transform;
        }
        
        protected override void OnExecute()
        {
            var pool = this.GetModel<IParticleEffectModel>().PoolDic.GetValue(ParticleType.RockShoot);
            var effect = pool.Allocate();
            
            effect.transform
                .Position(mEnemy.position)
                .LookAt(this.GetModel<IPlayerModel>().Transform);
            effect.GetComponentInChildren<RockShootController>().SetOwner(mEnemy);
            effect.SetActive(true);
        }
    }
}