using Controller.Character.Enemy.Enemy_Base.Controller;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Chief.Core;
using Controller.Character.Enemy.Chief.Chief_State.Ground_State;
using Controller.Character.Enemy.Chief.Chief_State.Skill_State;
using Controller.Character.Enemy.Chief.Chief_State.Stabbed_State;
using Controller.Character.Enemy.Chief.Chief_State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Chief.Controller
{
    public class ChiefController : EnemyController
    {
        private bool mApplyRootMotion = true;
        
        protected override EnemyState StartState => GetState<ChiefIdleState>();
        protected override EnemyCore InitCore => new ChiefCore();

        protected override void OnAnimatorMove()
        {
            if (!mApplyRootMotion)
            {
                return;
            }
            
            Vector3 position = mAnimator.rootPosition;
            mAgent.nextPosition = new Vector3(position.x, mAgent.nextPosition.y, position.z);
            position.y = mAgent.nextPosition.y;
            transform.position = position;
            
            Quaternion rotation = mAnimator.rootRotation;
            transform.rotation = rotation;
        }

        protected override void InitializeFSM()
        {
            RegisterState(new ChiefIdleState("Idle"));
            RegisterState(new ChiefChaseState("Chase"));
            RegisterState(new ChiefDeathState("Death"));
            RegisterState(new ChiefWalkAroundState("Walk Around"));
            
            RegisterState(new ChiefAttack1State("Attack 1"));
            RegisterState(new ChiefAttack2State("Attack 2"));
            
            RegisterState(new ChiefShieldBreakState("Shield Break"));
            RegisterState(new ChiefStabbedDeathSate("Stabbed Death"));
            RegisterState(new ChiefStabbedRaiseState("Stabbed Raise"));
            RegisterState(new ChiefStabbedState("Stabbed"));
            
            RegisterState(new ChiefTurnLeftState("Turn Left"));
            RegisterState(new ChiefTurnRightState("Turn Right"));
            RegisterState(new ChiefTurnBackState("Turn Back"));
        }
    }
}
