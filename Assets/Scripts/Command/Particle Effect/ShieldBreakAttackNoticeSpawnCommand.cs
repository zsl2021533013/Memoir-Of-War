using Architecture;
using Extension;
using Model.Particle_Effect;
using QFramework;
using UnityEngine;

namespace Command.Particle_Effect
{
    public class ShieldBreakAttackNoticeSpawnCommand : AbstractCommand
    {
        private Transform mEnemy;

        public ShieldBreakAttackNoticeSpawnCommand(Transform transform)
        {
            mEnemy = transform;
        }

        protected override void OnExecute()
        {
            var particle = this.GetModel<IParticleEffectModel>()
                .PoolDic
                .GetValue(ParticleType.ShieldBreakAttackNotice)
                .Allocate();
            var scale = mEnemy.GetChild(0).localScale.y;
            particle.Parent(mEnemy)
                .LocalPosition(new Vector3(0, 2 * scale, 0));
            particle.SetActive(true);
        }
    }
}