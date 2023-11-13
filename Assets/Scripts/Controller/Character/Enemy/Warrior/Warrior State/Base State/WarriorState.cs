using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Warrior.Warrior_State.Ground_State;

namespace Controller.Character.Enemy.Warrior.Warrior_State.Base_State
{
    public class WarriorState : EnemyState
    {
        public WarriorState(string animationName) : base(animationName)
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (core.IsDead)
            {
                core.ResetDeath();
                TranslateToState(controller.GetState<WarriorDeathState>());
            }
        }
    }
}