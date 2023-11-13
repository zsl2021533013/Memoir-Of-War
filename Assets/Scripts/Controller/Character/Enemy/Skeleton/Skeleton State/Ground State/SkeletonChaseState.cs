using Controller.Character.Enemy.Skeleton.Skeleton_State.Base_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Skill_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Skeleton.Skeleton_State.Ground_State
{
    public class SkeletonChaseState : SkeletonState
    {
        public SkeletonChaseState(string animationName) : base(animationName)
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
            
            if (core.HasArrived())
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
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.DisableNavMeshAgentRotation();
        }
    }
}