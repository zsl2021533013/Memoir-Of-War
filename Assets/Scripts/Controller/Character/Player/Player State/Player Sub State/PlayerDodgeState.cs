using System.Input_System;
using Controller.Character.Player.Player_State.Player_Ground_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Attack_State;
using UnityEngine;

namespace Controller.Character.Player.Player_State.Player_Sub_State
{
    public class PlayerDodgeState : PlayerSkillState
    {
        private Transform mDefenceColliderParent;
        private bool mIsColliderConnected;
        
        public PlayerDodgeState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            core.DisconnectDefenceCollider(out mDefenceColliderParent);

            mIsColliderConnected = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            if (isStateOver)
            {
                return;
            }

            if (core.IsAnimationEnd && !mIsColliderConnected)
            {
                core.ConnectDefenceCollider(mDefenceColliderParent);
                
                mIsColliderConnected = true;
            }

            if (core.IsAnimationEnd && InputKit.Instance.Attack)
            {
                TranslateToState(controller.GetState<PlayerAttack1State>());
            }
            
            if (core.IsAnimationEnd && InputKit.Instance.Movement.magnitude > 0.1f)
            {
                TranslateToState(controller.GetState<PlayerGroundState>());
            } // 不能写入父类做继承，因为这个转换优先级最低，必须写在最后
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.ConnectDefenceCollider(mDefenceColliderParent);
            
            mIsColliderConnected = true;
        }
    }
}