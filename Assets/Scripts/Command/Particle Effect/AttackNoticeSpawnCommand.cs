using Architecture;
using Extension;
using Model.Particle_Effect;
using QFramework;
using UnityEngine;

namespace Command.Particle_Effect
{
    public class AttackNoticeSpawnCommand : AbstractCommand
    // 不太规范，攻击提示应该直接挂载在敌人物体下方，而非通过命令生成
    {
        private Transform mEnemy;

        public AttackNoticeSpawnCommand(Transform transform)
        {
            mEnemy = transform;
        }

        protected override void OnExecute()
        {
            var particle = this.GetModel<IParticleEffectModel>()
                .PoolDic
                .GetValue(ParticleType.AttackNotice)
                .Allocate();
            var scale = mEnemy.GetChild(0).localScale.y;
            particle.Parent(mEnemy)
                .LocalPosition(new Vector3(0, 2 * scale, 0));
            particle.SetActive(true);
        }
    }
}