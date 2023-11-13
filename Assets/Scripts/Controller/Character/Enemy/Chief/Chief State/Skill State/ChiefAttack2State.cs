using Controller.Character.Enemy.Chief.Chief_State.Ground_State;
using Controller.Character.Enemy.Chief.Chief_State.Stabbed_State;
using Controller.Character.Enemy.Chief.Chief_State.Sub_State;
using Controller.Character.Enemy.Chief.Core;
using DG.Tweening;

namespace Controller.Character.Enemy.Chief.Chief_State.Skill_State
{
    public class ChiefAttack2State : ChiefSkillState
    {
        private bool isAttack2End = false;
        
        public ChiefAttack2State(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            isAttack2End = false;
            
            (core as ChiefCore).EnableNavMeshAgentPosition();

            DOVirtual.DelayedCall(2f, () => isAttack2End = true);
        }

        public override void OnUpdate()
        {
            if (!core.IsAnimationEnd && !core.HasArrived())
            {
                (core as ChiefCore).EnableNavMeshAgentPosition();
            }

            if (!core.IsAnimationEnd && core.HasArrived())
            {
                (core as ChiefCore).DisableNavMeshAgentPosition();
            }
            
            base.OnUpdate();
            
            if (isStateOver)
            {
                return;
            }

            if (isAttack2End)
            {
                (core as ChiefCore).Attack2End();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            
            (core as ChiefCore).DisableNavMeshAgentPosition();
        }
    }
}