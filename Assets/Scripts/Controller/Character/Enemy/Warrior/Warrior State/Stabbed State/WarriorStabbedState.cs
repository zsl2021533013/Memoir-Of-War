using Controller.Character.Enemy.Warrior.Warrior_State.Base_State;

namespace Controller.Character.Enemy.Warrior.Warrior_State.Stabbed_State
{
    public class WarriorStabbedState : WarriorState
    {
        public WarriorStabbedState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<WarriorStabbedDeathSate>());
            }
            else
            {
                TranslateToState(controller.GetState<WarriorStabbedRaiseState>());
            }
        }
    }
}