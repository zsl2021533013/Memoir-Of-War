using Controller.Character.Enemy.Red_Demon.Core;
using Controller.Character.Enemy.Red_Demon.State.Base_State;
using Controller.Character.Enemy.Red_Demon.State.Ground_State;
using Controller.Character.Enemy.Red_Demon.State.Stabbed_State;

namespace Controller.Character.Enemy.Red_Demon.State.Sub_State
{
    public class RedDemonSkillState : RedDemonState
    {
        public RedDemonSkillState(string animationName) : base(animationName)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            (core as RedDemonCore)?.ExtendSword();
            
            core.EnableNavMeshAgentRotation();
        }
        
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (core.IsShieldBreak)
            {
                TranslateToState(controller.GetState<RedDemonShieldBreakState>());
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                TranslateToState(controller.GetState<RedDemonWalkAroundState>());
            }

            if (core.IsAnimationEnd && !core.HasArrived())
            {
                TranslateToState(controller.GetState<RedDemonChaseState>());
            }
        }
        
        public override void OnExit()
        {
            base.OnExit();
            
            (core as RedDemonCore)?.ContractSword();
            
            core.DisableNavMeshAgentRotation();
        }
    }
}