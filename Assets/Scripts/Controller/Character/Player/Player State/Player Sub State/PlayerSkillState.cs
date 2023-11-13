using System.Input_System;
using Controller.Character.Player.Player_State.Player_Base_State;
using Controller.Character.Player.Player_State.Player_Ground_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Doge_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Knock_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Parry_State;
using UnityEngine;

namespace Controller.Character.Player.Player_State.Player_Sub_State
{
    public class PlayerSkillState : PlayerState
    {
        private Transform mTarget;
        
        public PlayerSkillState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            core.UseHurt(); // 清除外界输入
            
            mTarget = core.GetTheClosestEnemy();
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
                if (core.CheckStabbed())
                {
                    TranslateToState(controller.GetState<PlayerStabState>());
                }
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
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (core.IsAnimationEnd)
            {
                return;
            }
            
            core.RotatePlayerTowardsEnemy(mTarget);
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.UseHurt(); // 清除外界输入
        }
    }
}