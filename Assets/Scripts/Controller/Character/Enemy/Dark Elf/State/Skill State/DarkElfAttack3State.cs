using Controller.Character.Enemy.Dark_Elf.Core;
using Controller.Character.Enemy.Dark_Elf.State.Sub_State;

namespace Controller.Character.Enemy.Dark_Elf.State.Skill_State
{
    public class DarkElfAttack3State : DarkElfSkillState
    {
        public DarkElfAttack3State(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            var darkElfCore = (DarkElfCore)core;
            
            var pos = darkElfCore.GetFarTeleportPosition();

            darkElfCore.Teleport(pos);
        }
    }
}