using Architecture;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Skeleton.Core;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Base_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Skill_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Skeleton.Skeleton_State.Ground_State
{
    public class SkeletonWalkAroundState : SkeletonState
    {
        public SkeletonWalkAroundState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            switch (core.GetPlayerAngle())
            {
                case < 45:
                    break;
                case < 135:
                    TranslateToState(controller.GetState<SkeletonTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<SkeletonTurnBackState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<SkeletonTurnRightState>());
                    return;
            }

            core.EnableNavMeshAgentRotation();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }
            
            if (!core.DetectCanWalkAround())
            {
                core.SetWalkAroundType(WalkAroundType.Idle);
            }

            if (Time.time > startTime + MemoirOfWarAsset.WalkAroundTime)
            {
                core.AnimationEnd();
            }

            switch (core.GetPlayerAngle())
            {
                case < 45:
                    break;
                case < 135:
                    TranslateToState(controller.GetState<SkeletonTurnLeftState>());
                    break;
                case < 225:
                    TranslateToState(controller.GetState<SkeletonTurnBackState>());
                    break;
                case < 315:
                    TranslateToState(controller.GetState<SkeletonTurnRightState>());
                    break;
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

        public override void OnExit()
        {
            base.OnExit();
            
            var type = Random.Range(0, 1000);
            switch (type)
            {
                case < 500:
                    core.SetWalkAroundType(WalkAroundType.Left);
                    break;
                default:
                    core.SetWalkAroundType(WalkAroundType.Right);
                    break;
            }
            
            core.DisableNavMeshAgentRotation();
        }
    }
}