using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Ground_State;

namespace Controller.Character.Enemy.Skeleton.Skeleton_State.Base_State
{
    public class SkeletonState : EnemyState
    {
        public SkeletonState(string animationName) : base(animationName)
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (core.IsDead)
            {
                core.ResetDeath();
                TranslateToState(controller.GetState<SkeletonDeathState>());
            }
        }
    }
}