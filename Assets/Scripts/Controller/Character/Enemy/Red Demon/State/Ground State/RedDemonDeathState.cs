using Controller.Character.Enemy.Red_Demon.State.Base_State;

namespace Controller.Character.Enemy.Red_Demon.State.Ground_State
{
    public class RedDemonDeathState : RedDemonState
    {
        public RedDemonDeathState(string animationName) : base(animationName)
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