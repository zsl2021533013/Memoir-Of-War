using System.Input_System;
using Controller.Character.Player.Controller;
using Controller.Character.Player.Core;
using Controller.Character.Player.FSM.State_Machine;
using UnityEngine;

namespace Controller.Character.Player.Player_State.Player_Base_State
{
    public class PlayerState
    {
        private PlayerStateMachine stateMachine;
        
        protected PlayerController controller;
        protected PlayerCore core;
        protected string animationName;
        protected bool isStateOver = false;

        public PlayerState(string animationName)
        {
            this.animationName = animationName;
        }

        public virtual void OnEnter()
        {
            isStateOver = false;
            core.Play(animationName)
                .AnimationStart();
            PhysicsCheck();
            Debug.Log("Enter " + animationName + " State");
        }

        public virtual void OnExit()
        {
            isStateOver = true;
            InputKit.Instance.ResetInput();
            // Debug.Log("Exit " + animationName + " State");
        }

        public virtual void OnUpdate()
        {
            if (core.IsDead && this is not PlayerDeathState)
            {
                core.UseDeath();
                TranslateToState(controller.GetState<PlayerDeathState>());
            }
        }

        public virtual void OnFixedUpdate()
        {
            PhysicsCheck();
        }

        public virtual void PhysicsCheck()
        {
        }

        public PlayerState SetController(PlayerController playerController)
        {
            this.controller = playerController;
            return this;
        } 
        
        public PlayerState SetCoreManager(PlayerCore playerCore)
        {
            this.core = playerCore;
            return this;
        }

        public PlayerState SetStateMachine(PlayerStateMachine playerStateMachine)
        {
            this.stateMachine = playerStateMachine;
            return this;
        }

        public void TranslateToState(PlayerState playerState)
        {
            if (isStateOver)
            {
                return;
            }
            
            stateMachine.TranslateToState(playerState);
        }
    }
}