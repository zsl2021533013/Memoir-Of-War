using Controller.Character.Enemy.Janissary.Core;
using Controller.Character.Enemy.Janissary.State.Base_State;
using Controller.Character.Enemy.Janissary.State.Ground_State;
using Controller.Character.Enemy.Janissary.State.Stabbed_State;

namespace Controller.Character.Enemy.Janissary.State.Sub_State
{
    public class JanissarySkillState : JanissaryState
    {
        public JanissarySkillState(string animationName) : base(animationName)
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
                
                TranslateToState(controller.GetState<JanissaryShieldBreakState>());
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                TranslateToState(controller.GetState<JanissaryWalkAroundState>());
            }

            if (core.IsAnimationEnd && !core.HasArrived())
            {
                TranslateToState(controller.GetState<JanissaryChaseState>());
            }
        }
        
        public override void OnExit()
        {
            base.OnExit();
            
            core.DisableNavMeshAgentRotation();
        }
    }
}