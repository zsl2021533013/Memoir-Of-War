using Controller.Character.Enemy.Dark_Elf.State.Base_State;
using Controller.Character.Enemy.Dark_Elf.State.Ground_State;
using Controller.Character.Enemy.Dark_Elf.State.Skill_State;
using Controller.Character.Enemy.Dark_Elf.State.Stabbed_State;
using UnityEngine;

namespace Controller.Character.Enemy.Dark_Elf.State.Sub_State
{
    public class DarkElfSkillState : DarkElfState
    {
        public DarkElfSkillState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            core.EnableNavMeshAgentRotation();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (core.IsShieldBreak)
            {
                TranslateToState(controller.GetState<DarkElfShieldBreakState>());
            }

            if (core.IsAnimationEnd)
            {
                TranslateToState(controller.GetState<DarkElfWalkAroundState>());
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.DisableNavMeshAgentRotation();
        }
    }
}