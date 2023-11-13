using System.Input_System;
using Controller.Character.Player.Player_State.Player_Base_State;
using Controller.Character.Player.Player_State.Player_Ground_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Attack_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Doge_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Knock_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Parry_State;

namespace Controller.Character.Player.Player_State.Player_Sub_State
{
    public class PlayerControlledState : PlayerState
    {
        public PlayerControlledState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            core.UseControlled();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }
            
            // Controlled State 的 Animation End 放在 Raise 动画里
            
            if (core.IsAnimationEnd && core.IsKnockUp)
            {
                TranslateToState(controller.GetState<PlayerKnockUpState>());
            }
            if (core.IsAnimationEnd && core.IsKnockDown)
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

        public override void OnExit()
        {
            base.OnExit();
            
            core.UseControlled()
                .UseHurt();
        }
    }
}