using Controller.Character.Enemy.Dark_Elf.Core;
using Controller.Character.Enemy.Dark_Elf.State.Ground_State;
using Controller.Character.Enemy.Dark_Elf.State.Sub_State;

namespace Controller.Character.Enemy.Dark_Elf.State.Skill_State
{
    public class DarkElfAttack2State : DarkElfSkillState
    {
        public DarkElfAttack2State(string animationName) : base(animationName)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();

            var darkElfCore = (DarkElfCore)core;
            
            var pos = darkElfCore.GetCloseTeleportPosition();

            darkElfCore.Teleport(pos);
        }
    }
}