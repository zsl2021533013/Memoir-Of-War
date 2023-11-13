using Architecture;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Shaman.Core;
using Controller.Character.Enemy.Shaman.Shaman_State.Base_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Skill_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Shaman.Shaman_State.Ground_State
{
    public class ShamanWalkAroundState : ShamanState
    {
        public ShamanWalkAroundState(string animationName) : base(animationName)
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
                    TranslateToState(controller.GetState<ShamanTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<ShamanTurnBackState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<ShamanTurnRightState>());
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

            if (Time.time > startTime + MemoirOfWarAsset.IdleTime)
            {
                core.AnimationEnd();
            }

            switch (core.GetPlayerAngle())
            {
                case < 45:
                    break;
                case < 135:
                    TranslateToState(controller.GetState<ShamanTurnLeftState>());
                    break;
                case < 225:
                    TranslateToState(controller.GetState<ShamanTurnBackState>());
                    break;
                case < 315:
                    TranslateToState(controller.GetState<ShamanTurnRightState>());
                    break;
            }
            
            if (((ShamanCore)core).TooClose() && ((ShamanCore)core).DetectCanWalkAround())
            {
                TranslateToState(controller.GetState<ShamanWalkBackState>());
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                TranslateToState(controller.GetState<ShamanAttackState>());
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<ShamanChaseState>());
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