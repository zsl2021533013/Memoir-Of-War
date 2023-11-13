using Controller.Character.Enemy.Skeleton.Skeleton_State.Base_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Ground_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Turn_State;

namespace Controller.Character.Enemy.Skeleton.Skeleton_State.Stabbed_State
{
    public class SkeletonShieldBreakState : SkeletonState
    {
        public SkeletonShieldBreakState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            core.EnableStun();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (core.IsStabbed)
            {
                TranslateToState(controller.GetState<SkeletonStabbedState>());
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                TranslateToState(controller.GetState<SkeletonWalkAroundState>());
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<SkeletonChaseState>());
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.DisableStun()
                .ResetShieldBreak()
                .RecoverShield();
        }
    }
}