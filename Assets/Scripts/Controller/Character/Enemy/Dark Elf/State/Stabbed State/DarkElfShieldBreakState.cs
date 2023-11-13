using Controller.Character.Enemy.Dark_Elf.State.Base_State;
using Controller.Character.Enemy.Dark_Elf.State.Ground_State;

namespace Controller.Character.Enemy.Dark_Elf.State.Stabbed_State
{
    public class DarkElfShieldBreakState : DarkElfState
    {
        public DarkElfShieldBreakState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<DarkElfStabbedState>());
            }
            
            if (core.IsAnimationEnd)
            {
                TranslateToState(controller.GetState<DarkElfWalkAroundState>());
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