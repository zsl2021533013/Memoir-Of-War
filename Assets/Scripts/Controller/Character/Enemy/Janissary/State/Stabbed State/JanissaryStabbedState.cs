using Controller.Character.Enemy.Janissary.Core;
using Controller.Character.Enemy.Janissary.State.Base_State;

namespace Controller.Character.Enemy.Janissary.State.Stabbed_State
{
    public class JanissaryStabbedState : JanissaryState
    {
        public JanissaryStabbedState(string animationName) : base(animationName)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            core.ResetStabbed();
        }

        public override void OnUpdate()
        {
            if (isStateOver)
            {
                return;
            }

            if (!core.IsAnimationEnd) return;
            
            core.StabbedEnd();
                
            if (core.IsDead)
            {
                TranslateToState(controller.GetState<JanissaryStabbedDeathState>());
            }
            else
            {
                TranslateToState(controller.GetState<JanissaryStabbedRaiseState>());
            }
        }
    }
}