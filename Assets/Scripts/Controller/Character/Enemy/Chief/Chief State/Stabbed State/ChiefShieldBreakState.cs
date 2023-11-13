using Controller.Character.Enemy.Chief.Chief_State.Base_State;
using Controller.Character.Enemy.Chief.Chief_State.Ground_State;

namespace Controller.Character.Enemy.Chief.Chief_State.Stabbed_State
{
    public class ChiefShieldBreakState : ChiefState
    {
        public ChiefShieldBreakState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<ChiefStabbedState>());
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
            
            core.DisableStun()
                .ResetShieldBreak()
                .RecoverShield();
        }
    }
}