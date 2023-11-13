using Controller.Character.Enemy.Shaman.Shaman_State.Base_State;

namespace Controller.Character.Enemy.Shaman.Shaman_State.Stabbed_State
{
    public class ShamanStabbedState : ShamanState
    {
        public ShamanStabbedState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<ShamanStabbedDeathSate>());
            }
            else
            {
                TranslateToState(controller.GetState<ShamanStabbedRaiseState>());
            }
        }
    }
}