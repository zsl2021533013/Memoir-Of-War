using Controller.Character.Enemy.Red_Demon.State.Base_State;

namespace Controller.Character.Enemy.Red_Demon.State.Stabbed_State
{
    public class RedDemonStabbedState : RedDemonState
    {
        public RedDemonStabbedState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<RedDemonStabbedDeathState>());
            }
            else
            {
                TranslateToState(controller.GetState<RedDemonStabbedRaiseState>());
            }
        }
    }
}