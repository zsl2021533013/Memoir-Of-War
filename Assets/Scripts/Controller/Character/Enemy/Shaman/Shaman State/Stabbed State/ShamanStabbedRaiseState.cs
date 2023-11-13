using Controller.Character.Enemy.Shaman.Shaman_State.Base_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Ground_State;

namespace Controller.Character.Enemy.Shaman.Shaman_State.Stabbed_State
{
    public class ShamanStabbedRaiseState : ShamanState
    {
        public ShamanStabbedRaiseState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<ShamanWalkAroundState>());
            }
        }
    }
}