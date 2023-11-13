using Controller.Character.Enemy.Undead_Knight.State.Base_State;
using Controller.Character.Enemy.Undead_Knight.State.Ground_State;

namespace Controller.Character.Enemy.Undead_Knight.State.Stabbed_State
{
    public class UndeadKnightShieldBreakState : UndeadKnightState
    {
        public UndeadKnightShieldBreakState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<UndeadKnightStabbedState>());
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
            
            core.DisableStun()
                .ResetShieldBreak()
                .RecoverShield();
        }
    }
}