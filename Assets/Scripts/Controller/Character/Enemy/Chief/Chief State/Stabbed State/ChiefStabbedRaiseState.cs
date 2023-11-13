using Controller.Character.Enemy.Chief.Chief_State.Base_State;
using Controller.Character.Enemy.Chief.Chief_State.Ground_State;

namespace Controller.Character.Enemy.Chief.Chief_State.Stabbed_State
{
    public class ChiefStabbedRaiseState : ChiefState
    {
        public ChiefStabbedRaiseState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<ChiefWalkAroundState>());
            }
        }
    }
}