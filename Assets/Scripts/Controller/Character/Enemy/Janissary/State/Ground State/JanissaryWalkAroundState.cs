using Architecture;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Janissary.Core;
using Controller.Character.Enemy.Janissary.State.Base_State;
using Controller.Character.Enemy.Janissary.State.Skill_State;
using Controller.Character.Enemy.Janissary.State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Janissary.State.Ground_State
{
    public class JanissaryWalkAroundState : JanissaryState
    {
        public JanissaryWalkAroundState(string animationName) : base(animationName)
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
                    TranslateToState(controller.GetState<JanissaryTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<JanissaryTurnRightState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<JanissaryTurnRightState>());
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
                    TranslateToState(controller.GetState<JanissaryTurnLeftState>());
                    break;
                case < 225:
                    TranslateToState(controller.GetState<JanissaryTurnRightState>());
                    break;
                case < 315:
                    TranslateToState(controller.GetState<JanissaryTurnRightState>());
                    break;
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                var attackType = Random.Range(0, 900);
                switch (attackType)
                {
                    case < 300:
                        TranslateToState(controller.GetState<JanissaryAttack1State>());
                        break;
                    case < 600:
                        TranslateToState(controller.GetState<JanissaryAttack2State>());
                        break;
                    default:
                        TranslateToState(controller.GetState<JanissaryAttack3State>());
                        break;
                }
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<JanissaryChaseState>());
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