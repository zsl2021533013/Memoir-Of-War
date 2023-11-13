using Controller.Character.Enemy.Skeleton.Skeleton_State.Base_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Ground_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Turn_State;

namespace Controller.Character.Enemy.Skeleton.Skeleton_State.Stabbed_State
{
    public class SkeletonStabbedRaiseState : SkeletonState
    {
        public SkeletonStabbedRaiseState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<SkeletonWalkAroundState>());
            }
        }
    }
}