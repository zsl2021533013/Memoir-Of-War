using Controller.Character.Enemy.Red_Demon.State.Base_State;
using Controller.Character.Enemy.Red_Demon.State.Ground_State;

namespace Controller.Character.Enemy.Red_Demon.State.Stabbed_State
{
    public class RedDemonStabbedRaiseState : RedDemonState
    {
        public RedDemonStabbedRaiseState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<RedDemonWalkAroundState>());
            }
        }
    }
}