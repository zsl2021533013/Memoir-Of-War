using Controller.Character.Enemy.Enemy_Base.Controller;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Enemy_Base.FSM;
using UnityEngine;

namespace Controller.Character.Enemy.Enemy_Base.State
{
    public class EnemyState
    {
        private EnemyStateMachine mStateMachine;
        protected EnemyController controller;
        protected EnemyCore core;
        
        protected bool isStateOver = false;
        protected float startTime;
        protected string animationName;

        public EnemyState(string animationName)
        {
            this.animationName = animationName;
        }

        public virtual void OnEnter()
        {
            isStateOver = false;
            startTime = Time.time;
            core.Play(animationName)
                .AnimationStart();
            PhysicsCheck();
            // Debug.Log("Enter " + animationName + " State");
        }

        public virtual void OnExit()
        {
            isStateOver = true;
            controller.CloseWeapon();
            // Debug.Log("Exit " + animationName + " State");
        }

        public virtual void OnUpdate()
        {
            if (isStateOver)
            {
                return;
            }

            core.SetDestination();
        }

        public virtual void OnFixedUpdate()
        {
            PhysicsCheck();
        }

        public virtual void PhysicsCheck()
        {
        }

        public EnemyState SetController(EnemyController controller)
        {
            this.controller = controller;
            return this;
        } 
        
        public EnemyState SetCoreManager(EnemyCore skeletonCore)
        {
            this.core = skeletonCore;
            return this;
        }

        public EnemyState SetStateMachine(EnemyStateMachine stateMachine)
        {
            this.mStateMachine = stateMachine;
            return this;
        }

        protected void TranslateToState(EnemyState state) 
            // TODO: 有点复杂了，应该直接写成泛型，传参的时候直接传一个类型就行
        {
            if (isStateOver)
            {
                return;
            }
            
            mStateMachine.TranslateToState(state);
        }
    }
}