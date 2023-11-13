using Controller.Character.Enemy.Chief.Chief_State.Base_State;

namespace Controller.Character.Enemy.Chief.Chief_State.Ground_State
{
    public class ChiefDeathState : ChiefState
    {
        public ChiefDeathState(string animationName) : base(animationName)
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