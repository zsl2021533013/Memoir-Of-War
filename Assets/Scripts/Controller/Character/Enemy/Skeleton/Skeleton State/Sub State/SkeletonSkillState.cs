using Controller.Character.Enemy.Skeleton.Skeleton_State.Base_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Ground_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Stabbed_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Turn_State;

namespace Controller.Character.Enemy.Skeleton.Skeleton_State.Sub_State
{
    public class SkeletonSkillState : SkeletonState
    {
        public SkeletonSkillState(string animationName) : base(animationName)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            core.EnableNavMeshAgentRotation();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }
            
            if (core.IsShieldBreak)
            {
                TranslateToState(controller.GetState<SkeletonShieldBreakState>());
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
            
            core.DisableNavMeshAgentRotation();
        }
    }
}