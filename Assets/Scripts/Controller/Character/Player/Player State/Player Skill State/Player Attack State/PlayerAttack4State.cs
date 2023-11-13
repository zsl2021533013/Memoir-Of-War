using System.Input_System;
using Controller.Character.Player.Player_State.Player_Ground_State;
using Controller.Character.Player.Player_State.Player_Sub_State;

namespace Controller.Character.Player.Player_State.Player_Skill_State.Player_Attack_State
{
    public class PlayerAttack4State : PlayerSkillState
    {
        public PlayerAttack4State(string animationName) : base(animationName)
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
            
            if (core.IsAnimationEnd && InputKit.Instance.Movement.magnitude > 0.1f)
            {
                TranslateToState(controller.GetState<PlayerGroundState>());
            } // 不能写入父类做继承，因为这个转换优先级最低，必须写在最后
        }
    }
}