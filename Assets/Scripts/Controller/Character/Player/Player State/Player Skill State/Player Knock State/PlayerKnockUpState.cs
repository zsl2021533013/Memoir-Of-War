using System.Input_System;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Attack_State;
using Controller.Character.Player.Player_State.Player_Sub_State;

namespace Controller.Character.Player.Player_State.Player_Skill_State.Player_Knock_State
{
    public class PlayerKnockUpState : PlayerControlledState
    {
        public PlayerKnockUpState(string animationName) : base(animationName)
        {
        }
        
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }
            
            if (core.IsAnimationEnd && InputKit.Instance.Attack)
            {
                TranslateToState(controller.GetState<PlayerAttack1State>());
            }
        }
    }
}