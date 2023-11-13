using Controller.Character.Enemy.Enemy_Base.Controller;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Warrior.Core;
using Controller.Character.Enemy.Warrior.Warrior_State.Ground_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Skill_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Stabbed_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Turn_State;

namespace Controller.Character.Enemy.Warrior.Controller
{
    public class WarriorController : EnemyController 
    {
        protected override EnemyState StartState => GetState<WarriorIdleState>();
        protected override EnemyCore InitCore => new WarriorCore();

        protected override void InitializeFSM()
        {
            RegisterState(new WarriorIdleState("Idle"));
            RegisterState(new WarriorChaseState("Chase"));
            RegisterState(new WarriorDeathState("Death"));
            RegisterState(new WarriorWalkAroundState("Walk Around"));
            
            RegisterState(new WarriorAttack1State("Attack 1"));
            RegisterState(new WarriorAttack2State("Attack 2"));
            
            RegisterState(new WarriorShieldBreakState("Shield Break"));
            RegisterState(new WarriorStabbedDeathSate("Stabbed Death"));
            RegisterState(new WarriorStabbedRaiseState("Stabbed Raise"));
            RegisterState(new WarriorStabbedState("Stabbed"));
            
            RegisterState(new WarriorTurnLeftState("Turn Left"));
            RegisterState(new WarriorTurnRightState("Turn Right"));
            RegisterState(new WarriorTurnBackState("Turn Back"));
        }
    }
}
