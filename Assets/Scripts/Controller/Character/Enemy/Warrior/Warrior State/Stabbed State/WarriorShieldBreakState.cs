using Controller.Character.Enemy.Warrior.Warrior_State.Base_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Ground_State;

namespace Controller.Character.Enemy.Warrior.Warrior_State.Stabbed_State
{
    public class WarriorShieldBreakState : WarriorState
    {
        public WarriorShieldBreakState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<WarriorStabbedState>());
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                TranslateToState(controller.GetState<WarriorWalkAroundState>());
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<WarriorChaseState>());
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