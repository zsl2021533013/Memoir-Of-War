using Controller.Character.Enemy.Janissary.State.Base_State;
using Controller.Character.Enemy.Janissary.State.Ground_State;

namespace Controller.Character.Enemy.Janissary.State.Stabbed_State
{
    public class JanissaryStabbedRaiseState : JanissaryState
    {
        public JanissaryStabbedRaiseState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<JanissaryWalkAroundState>());
            }
        }
    }
}