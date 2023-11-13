using Architecture;
using Controller.Particle_Effect;
using Extension;
using Model.Particle_Effect;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Command.Particle_Effect
{
    public class EnemyFireballSpawnCommand : AbstractCommand
    {
        private Transform mEnemy;
        private Vector3 mPos;

        public EnemyFireballSpawnCommand(Transform transform, Vector3 pos)
        {
            mEnemy = transform;
            mPos = pos;
        }
        
        protected override void OnExecute()
        {
            Debug.Log("Shoot fireball");
            
            var pool = this.GetModel<IParticleEffectModel>().PoolDic.GetValue(ParticleType.EnemyFireball);
            var effect = pool.Allocate();
            
            effect.transform
                .Position(mPos)
                .LookAt(this.GetModel<IPlayerModel>().Transform);
            effect.GetComponentInChildren<EnemyFireballController>().SetOwner(mEnemy);
            effect.SetActive(true);
        }
    }
}