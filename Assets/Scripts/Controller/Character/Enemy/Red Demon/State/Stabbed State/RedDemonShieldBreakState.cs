using Controller.Character.Enemy.Red_Demon.State.Base_State;
using Controller.Character.Enemy.Red_Demon.State.Ground_State;

namespace Controller.Character.Enemy.Red_Demon.State.Stabbed_State
{
    public class RedDemonShieldBreakState : RedDemonState
    {
        public RedDemonShieldBreakState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<RedDemonStabbedState>());
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

            core.DisableStun()
                .ResetShieldBreak()
                .RecoverShield();
        }
    }
}