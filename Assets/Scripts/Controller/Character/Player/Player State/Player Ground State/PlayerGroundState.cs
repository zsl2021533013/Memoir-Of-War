using System.Input_System;
using Controller.Character.Player.Player_State.Player_Base_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Attack_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Doge_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Knock_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Parry_State;

namespace Controller.Character.Player.Player_State.Player_Ground_State
{
    public class PlayerGroundState : PlayerState
    {
        public PlayerGroundState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            core.DisableRootMotion();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (core.IsTimelineEnable)
            {
                TranslateToState(controller.GetState<PlayerMoveForwardState>());
            }
            
            if (core.IsKnockUp)
            {
                TranslateToState(controller.GetState<PlayerKnockUpState>());
            }
            if (core.IsKnockDown)
            {
                TranslateToState(controller.GetState<PlayerKnockDownState>());
            }
            
            if (core.IsHurt)
            {
                TranslateToState(controller.GetState<PlayerHurtState>());
            }
            
            if (InputKit.Instance.Attack)
            {
                if (core.CheckStabbed())
                {
                    TranslateToState(controller.GetState<PlayerStabState>());
                }
                else
                {
                    TranslateToState(controller.GetState<PlayerAttack1State>());
                }
            }
            if (InputKit.Instance.Defence)
            {
                TranslateToState(controller.GetState<PlayerDefenceState>());
            }
            if (InputKit.Instance.Dodge)
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
        }
        
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            
            core.MovePlayer() // 修改动画状态机，改变根运动
                .RotatePlayer(); // 旋转玩家
        }

        public override void OnExit()
        {
            base.OnExit();

            core.EnableRootMotion()
                .ResetAnimatorSpeedValue();
        }
    }
}