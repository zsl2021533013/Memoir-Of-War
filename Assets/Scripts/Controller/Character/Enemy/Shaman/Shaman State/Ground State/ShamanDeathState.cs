using Controller.Character.Enemy.Shaman.Shaman_State.Base_State;

namespace Controller.Character.Enemy.Shaman.Shaman_State.Ground_State
{
    public class ShamanDeathState : ShamanState
    {
        public ShamanDeathState(string animationName) : base(animationName)
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