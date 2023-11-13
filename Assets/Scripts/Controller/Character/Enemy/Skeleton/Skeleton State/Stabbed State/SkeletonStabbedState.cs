using Controller.Character.Enemy.Skeleton.Skeleton_State.Base_State;

namespace Controller.Character.Enemy.Skeleton.Skeleton_State.Stabbed_State
{
    public class SkeletonStabbedState : SkeletonState
    {
        public SkeletonStabbedState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<SkeletonStabbedDeathSate>());
            }
            else
            {
                TranslateToState(controller.GetState<SkeletonStabbedRaiseState>());
            }
        }
    }
}