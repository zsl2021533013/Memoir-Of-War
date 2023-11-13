using Controller.Character.Enemy.Undead_Knight.State.Base_State;
using Controller.Character.Enemy.Undead_Knight.State.Ground_State;
using Controller.Character.Enemy.Undead_Knight.State.Stabbed_State;

namespace Controller.Character.Enemy.Undead_Knight.State.Sub_State
{
    public class UndeadKnightSkillState : UndeadKnightState
    {
        public UndeadKnightSkillState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<UndeadKnightShieldBreakState>());
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                TranslateToState(controller.GetState<UndeadKnightWalkAroundState>());
            }

            if (core.IsAnimationEnd && !core.HasArrived())
            {
                TranslateToState(controller.GetState<UndeadKnightChaseState>());
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.DisableNavMeshAgentRotation();
        }
    }
}