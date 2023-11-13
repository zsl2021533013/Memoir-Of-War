using Controller.Character.Enemy.Janissary.State.Base_State;

namespace Controller.Character.Enemy.Janissary.State.Ground_State
{
    public class JanissaryDeathState : JanissaryState
    {
        public JanissaryDeathState(string animationName) : base(animationName)
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