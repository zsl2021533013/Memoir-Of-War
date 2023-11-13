using Controller.Character.Enemy.Dark_Elf.State.Base_State;

namespace Controller.Character.Enemy.Dark_Elf.State.Ground_State
{
    public class DarkElfDeathState : DarkElfState
    {
        public DarkElfDeathState(string animationName) : base(animationName)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();

            isStateOver = true;
            
            controller.CloseWeapon();
        }

        public override void OnExit()
        {
            base.OnExit();

            core.ResetAllExternalInput();
        }
    }
}