using Controller.Character.Enemy.Undead_Knight.State.Base_State;

namespace Controller.Character.Enemy.Undead_Knight.State.Stabbed_State
{
    public class UndeadKnightStabbedState : UndeadKnightState
    {
        public UndeadKnightStabbedState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<UndeadKnightStabbedDeathState>());
            }
            else
            {
                TranslateToState(controller.GetState<UndeadKnightStabbedRaiseState>());
            }
        }
    }
}