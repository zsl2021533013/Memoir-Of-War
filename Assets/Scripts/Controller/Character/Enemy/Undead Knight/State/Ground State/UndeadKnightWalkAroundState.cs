using Architecture;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Undead_Knight.Core;
using Controller.Character.Enemy.Undead_Knight.State.Skill_State;
using Controller.Character.Enemy.Undead_Knight.State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Undead_Knight.State.Ground_State
{
    public class UndeadKnightWalkAroundState : EnemyState
    {
        public UndeadKnightWalkAroundState(string animationName) : base(animationName)
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
                    TranslateToState(controller.GetState<UndeadKnightTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<UndeadKnightTurnBackState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<UndeadKnightTurnRightState>());
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
                    TranslateToState(controller.GetState<UndeadKnightTurnLeftState>());
                    break;
                case < 225:
                    TranslateToState(controller.GetState<UndeadKnightTurnBackState>());
                    break;
                case < 315:
                    TranslateToState(controller.GetState<UndeadKnightTurnRightState>());
                    break;
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                var attackType = Random.Range(0, 1000);
                switch (attackType)
                {
                    case < 350:
                        TranslateToState(controller.GetState<UndeadKnightAttack1State>());
                        break;
                    case < 700:
                        TranslateToState(controller.GetState<UndeadKnightAttack2State>());
                        break;
                    default:
                        TranslateToState(controller.GetState<UndeadKnightAttack3State>());
                        break;
                }
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<UndeadKnightChaseState>());
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