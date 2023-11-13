using System.Input_System;
using Controller.Character.Player.Player_State.Player_Base_State;
using Controller.Character.Player.Player_State.Player_Ground_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Attack_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Doge_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Knock_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Parry_State;
using UnityEngine;

namespace Controller.Character.Player.Player_State
{
    public class PlayerHurtState : PlayerState
    {
        public PlayerHurtState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            core.UseHurt();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }
            
            if (core.IsKnockUp)
            {
                TranslateToState(controller.GetState<PlayerKnockUpState>());
            }
            if (core.IsKnockDown)
            {
                TranslateToState(controller.GetState<PlayerKnockDownState>());
            }
            
            if (core.IsAnimationEnd && core.IsHurt)
            {
                TranslateToState(controller.GetState<PlayerHurtState>());
            }
            
            if (core.IsAnimationEnd && InputKit.Instance.Attack)
            {
                TranslateToState(controller.GetState<PlayerAttack1State>());
            }
            if (core.IsAnimationEnd && InputKit.Instance.Defence)
            {
                TranslateToState(controller.GetState<PlayerDefenceState>());
            }
            if (core.IsAnimationEnd && InputKit.Instance.Dodge)
            {
                switch (core.GetDodgeDirection())
                {
                    case Direction.Left:
                        TranslateToState(controller.GetState<PlayerDodgeLeftState>());
                        break;
                    case Direction.Right:
                        TranslateToState(controller.GetState<PlayerDodgeRightState>());
                        break;
                    case Direction.Forward:
                    case Direction.Backward:
                    default:
                        TranslateToState(controller.GetState<PlayerDodgeRightState>());
                        break;
                }
            }
            
            if (core.IsAnimationEnd && InputKit.Instance.Movement.magnitude > 0.1f)
            {
                TranslateToState(controller.GetState<PlayerGroundState>());
            }
        }
    }
}