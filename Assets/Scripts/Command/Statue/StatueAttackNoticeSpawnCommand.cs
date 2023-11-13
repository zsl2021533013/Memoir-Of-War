using Architecture;
using Controller.Statue;
using Extension;
using Model.Particle_Effect;
using QFramework;
using UnityEngine;

namespace Command.Statue
{
    public class StatueAttackNoticeSpawnCommand : AbstractCommand
    {
        private Transform mStatue;
        private StatueAttackType mType;

        public StatueAttackNoticeSpawnCommand(Transform statue, StatueAttackType type)
        {
            mStatue = statue;
            mType = type;
        }
        
        protected override void OnExecute()
        {
            var particleType = mType == StatueAttackType.Normal
                ? ParticleType.AttackNotice
                : ParticleType.ShieldBreakAttackNotice;
            
            var particle = this.GetModel<IParticleEffectModel>()
                .PoolDic
                .GetValue(particleType)
                .Allocate();
            particle.Parent(mStatue)
                .LocalPosition(new Vector3(0f, 3.5f, 0f));
            particle.SetActive(true);
        }
    }
}