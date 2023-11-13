using Controller.Character.Enemy.Dark_Elf.State.Base_State;
using Controller.Character.Enemy.Dark_Elf.State.Ground_State;

namespace Controller.Character.Enemy.Dark_Elf.State.Stabbed_State
{
    public class DarkElfStabbedRaiseState : DarkElfState
    {
        public DarkElfStabbedRaiseState(string animationName) : base(animationName)
        {
        }
        
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (core.IsAnimationEnd)
            {
                TranslateToState(controller.GetState<DarkElfWalkAroundState>());
            }
        }
    }
}