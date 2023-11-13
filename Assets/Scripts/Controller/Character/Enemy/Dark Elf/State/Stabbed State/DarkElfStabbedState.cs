using Controller.Character.Enemy.Dark_Elf.State.Base_State;

namespace Controller.Character.Enemy.Dark_Elf.State.Stabbed_State
{
    public class DarkElfStabbedState : DarkElfState
    {
        public DarkElfStabbedState(string animationName) : base(animationName)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            core.ResetStabbed();
        }

        public override void OnUpdate()
        {
            if (isStateOver)
            {
                return;
            }

            if (!core.IsAnimationEnd) return;
            
            core.StabbedEnd();
                
            if (core.IsDead)
            {
                TranslateToState(controller.GetState<DarkElfStabbedDeathState>());
            }
            else
            {
                TranslateToState(controller.GetState<DarkElfStabbedRaiseState>());
            }
        }
    }
}