using Controller.Character.Enemy.Skeleton.Skeleton_State.Base_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Ground_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Skill_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Skeleton.Skeleton_State.Sub_State
{
    public class SkeletonTurnState : SkeletonState
    {
        public SkeletonTurnState(string animationName) : base(animationName)
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