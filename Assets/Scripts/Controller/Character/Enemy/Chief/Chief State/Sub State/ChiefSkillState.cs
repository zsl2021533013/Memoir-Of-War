using Controller.Character.Enemy.Chief.Chief_State.Base_State;
using Controller.Character.Enemy.Chief.Chief_State.Ground_State;
using Controller.Character.Enemy.Chief.Chief_State.Stabbed_State;

namespace Controller.Character.Enemy.Chief.Chief_State.Sub_State
{
    public class ChiefSkillState : ChiefState
    {
        public ChiefSkillState(string animationName) : base(animationName)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
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
                TranslateToState(controller.GetState<ChiefShieldBreakState>());
            }
            

            if (core.IsAnimationEnd && core.HasArrived())
            {
                TranslateToState(controller.GetState<ChiefWalkAroundState>());
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<ChiefChaseState>());
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.DisableNavMeshAgentRotation();
        }
    }
}