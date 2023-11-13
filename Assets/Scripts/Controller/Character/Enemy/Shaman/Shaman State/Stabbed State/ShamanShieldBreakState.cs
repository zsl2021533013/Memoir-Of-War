using Controller.Character.Enemy.Shaman.Shaman_State.Base_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Ground_State;

namespace Controller.Character.Enemy.Shaman.Shaman_State.Stabbed_State
{
    public class ShamanShieldBreakState : ShamanState
    {
        public ShamanShieldBreakState(string animationName) : base(animationName)
        {
        }
        
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (core.IsStabbed)
            {
                TranslateToState(controller.GetState<ShamanStabbedState>());
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                TranslateToState(controller.GetState<ShamanWalkAroundState>());
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<ShamanChaseState>());
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.ResetShieldBreak();
            core.RecoverShield();
        }
    }
}