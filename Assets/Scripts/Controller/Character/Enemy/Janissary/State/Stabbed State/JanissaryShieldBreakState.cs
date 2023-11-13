using Controller.Character.Enemy.Janissary.State.Base_State;
using Controller.Character.Enemy.Janissary.State.Ground_State;

namespace Controller.Character.Enemy.Janissary.State.Stabbed_State
{
    public class JanissaryShieldBreakState : JanissaryState
    {
        public JanissaryShieldBreakState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            core.EnableStun();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (core.IsStabbed)
            {
                TranslateToState(controller.GetState<JanissaryStabbedState>());
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
            
            core.DisableStun()
                .ResetShieldBreak()
                .RecoverShield();
        }
    }
}