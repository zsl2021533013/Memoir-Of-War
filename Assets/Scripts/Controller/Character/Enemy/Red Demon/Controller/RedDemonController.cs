using Controller.Character.Enemy.Enemy_Base.Controller;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Red_Demon.Core;
using Controller.Character.Enemy.Red_Demon.State;
using Controller.Character.Enemy.Red_Demon.State.Ground_State;
using Controller.Character.Enemy.Red_Demon.State.Skill_State;
using Controller.Character.Enemy.Red_Demon.State.Stabbed_State;
using Controller.Character.Enemy.Red_Demon.State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Red_Demon.Controller
{
    public class RedDemonController : EnemyController
    {
        protected override EnemyState StartState => GetState<RedDemonIdleState>();

        protected override EnemyCore InitCore => new RedDemonCore();

        protected override void InitializeFSM()
        {
            RegisterState(new RedDemonIdleState("Idle"));
            RegisterState(new RedDemonChaseState("Chase"));
            RegisterState(new RedDemonDeathState("Death"));
            RegisterState(new RedDemonWalkAroundState("Walk Around"));
            
            RegisterState(new RedDemonAttack1State("Attack 1"));
            RegisterState(new RedDemonAttack2State("Attack 2"));
            RegisterState(new RedDemonAttack3State("Attack 3"));
            RegisterState(new RedDemonAttack4State("Attack 4"));
            
            RegisterState(new RedDemonShieldBreakState("Shield Break"));
            RegisterState(new RedDemonStabbedDeathState("Stabbed Death"));
            RegisterState(new RedDemonStabbedRaiseState("Stabbed Raise"));
            RegisterState(new RedDemonStabbedState("Stabbed"));
            
            RegisterState(new RedDemonTurnLeftState("Turn Left"));
            RegisterState(new RedDemonTurnRightState("Turn Right"));
        }
    }
}