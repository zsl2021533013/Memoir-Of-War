using Controller.Character.Enemy.Warrior.Warrior_State.Base_State;

namespace Controller.Character.Enemy.Warrior.Warrior_State.Ground_State
{
    public class WarriorDeathState : WarriorState
    {
        public WarriorDeathState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            isStateOver = true;
            
            controller.CloseWeapon();
        }

        public override void OnExit()
        {
            base.OnExit();

            core.ResetAllExternalInput();
        }
    }
}