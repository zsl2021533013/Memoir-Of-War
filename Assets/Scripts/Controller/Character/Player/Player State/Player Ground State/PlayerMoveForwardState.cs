using System.Input_System;
using Controller.Character.Player.Player_State.Player_Base_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Attack_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Doge_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Knock_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Parry_State;
using DG.Tweening;
using UnityEngine.PlayerLoop;

namespace Controller.Character.Player.Player_State.Player_Ground_State
{
    public class PlayerMoveForwardState : PlayerState
    {
        private bool mIsUsed = false;
        
        public PlayerMoveForwardState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            core.DisableRootMotion();

            mIsUsed = false;
        }
        
        public override void OnFixedUpdate()
        {
            base.OnUpdate();
            
            if (core.IsTimelineEnable && !mIsUsed)
            {
                core.TimelineMoveForward();
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (!core.IsTimelineEnable && !mIsUsed)
            {
                mIsUsed = true;
                core.StopTimelineMove()
                    .OnComplete(() =>
                    {
                        TranslateToState(controller.GetState<PlayerGroundState>());
                    });
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            core.EnableRootMotion();
        }
    }
}