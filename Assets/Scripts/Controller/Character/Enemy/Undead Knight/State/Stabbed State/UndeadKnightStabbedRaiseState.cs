using Controller.Character.Enemy.Undead_Knight.State.Base_State;
using Controller.Character.Enemy.Undead_Knight.State.Ground_State;

namespace Controller.Character.Enemy.Undead_Knight.State.Stabbed_State
{
    public class UndeadKnightStabbedRaiseState : UndeadKnightState
    {
        public UndeadKnightStabbedRaiseState(string animationName) : base(animationName)
        {
        }
        
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (core.IsAnimationEnd)
            {
                TranslateToState(controller.GetState<UndeadKnightWalkAroundState>());
            }
        }
    }
}