using Controller.Character.Enemy.Chief.Chief_State.Base_State;

namespace Controller.Character.Enemy.Chief.Chief_State.Stabbed_State
{
    public class ChiefStabbedState : ChiefState
    {
        public ChiefStabbedState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<ChiefStabbedDeathSate>());
            }
            else
            {
                TranslateToState(controller.GetState<ChiefStabbedRaiseState>());
            }
        }
    }
}