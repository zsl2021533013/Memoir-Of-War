using Controller.Character.Enemy.Undead_Knight.State.Base_State;

namespace Controller.Character.Enemy.Undead_Knight.State.Ground_State
{
    public class UndeadKnightDeathState : UndeadKnightState
    {
        public UndeadKnightDeathState(string animationName) : base(animationName)
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