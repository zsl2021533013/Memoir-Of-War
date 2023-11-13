using Controller.Character.Enemy.Warrior.Warrior_State.Base_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Ground_State;

namespace Controller.Character.Enemy.Warrior.Warrior_State.Stabbed_State
{
    public class WarriorStabbedRaiseState : WarriorState
    {
        public WarriorStabbedRaiseState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<WarriorWalkAroundState>());
            }
        }
    }
}