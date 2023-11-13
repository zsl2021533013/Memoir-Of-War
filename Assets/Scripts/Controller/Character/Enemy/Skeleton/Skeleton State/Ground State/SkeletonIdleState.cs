using Architecture;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Base_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Skill_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Skeleton.Skeleton_State.Ground_State
{
    public class SkeletonIdleState : SkeletonState
    {
        public SkeletonIdleState(string animationName) : base(animationName)
        {
        }
        
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (Time.time > startTime + MemoirOfWarAsset.IdleTime)
            {
                core.AnimationEnd();
            }
            
            if (core.IsAnimationEnd && core.HasArrived())
            {
                var attackType = Random.Range(0, 1000);
                if (attackType < 500)
                {
                    TranslateToState(controller.GetState<SkeletonAttack1State>());
                }
                else
                {
                    TranslateToState(controller.GetState<SkeletonAttack2State>());
                }
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<SkeletonChaseState>());
            }
        }
    }
}