using Architecture;
using Extension;
using Model.Particle_Effect;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Command.Particle_Effect
{
    public class PlayerFireballSpawnCommand : AbstractCommand
    {
        private Transform mEnemy;
        private Vector3 mPos;

        public PlayerFireballSpawnCommand(Transform transform, Vector3 pos)
        {
            mEnemy = transform;
            mPos = pos;
        }
        
        protected override void OnExecute()
        {
            Debug.Log("Shoot fireball");

            var pool = this.GetModel<IParticleEffectModel>().PoolDic.GetValue(ParticleType.PlayerFireball);
            var effect = pool.Allocate();

            var targetPos = mEnemy.position;
            targetPos.y = mPos.y;
            
            effect.transform
                .Position(mPos)
                .LookAt(targetPos);
            
            effect.SetActive(true);
        }
    }
}