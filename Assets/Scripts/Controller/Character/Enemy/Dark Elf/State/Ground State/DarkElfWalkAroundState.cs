using Architecture;
using Controller.Character.Enemy.Dark_Elf.State.Base_State;
using Controller.Character.Enemy.Dark_Elf.State.Skill_State;
using Controller.Character.Enemy.Dark_Elf.State.Stabbed_State;
using Controller.Character.Enemy.Dark_Elf.State.Turn_State;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Enemy_Base.State;
using UnityEngine;

namespace Controller.Character.Enemy.Dark_Elf.State.Ground_State
{
    public class DarkElfWalkAroundState : DarkElfState
    {
        public DarkElfWalkAroundState(string animationName) : base(animationName)
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
                    TranslateToState(controller.GetState<DarkElfTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<DarkElfTurnBackState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<DarkElfTurnRightState>());
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
            
            if (core.IsShieldBreak)
            {
                TranslateToState(controller.GetState<DarkElfShieldBreakState>());
            }
            
            switch (core.GetPlayerAngle())
            {
                case < 45:
                    break;
                case < 135:
                    TranslateToState(controller.GetState<DarkElfTurnLeftState>());
                    break;
                case < 225:
                    TranslateToState(controller.GetState<DarkElfTurnBackState>());
                    break;
                case < 315:
                    TranslateToState(controller.GetState<DarkElfTurnRightState>());
                    break;
            }

            if (core.IsAnimationEnd)
            {
                var attackType = Random.Range(0, 1000);
                switch (attackType)
                {
                    case < 300:
                        TranslateToState(controller.GetState<DarkElfAttack1State>());
                        break;
                    case < 600:
                        TranslateToState(controller.GetState<DarkElfAttack2State>());
                        break;
                    default:
                        TranslateToState(controller.GetState<DarkElfAttack3State>());
                        break;
                }
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