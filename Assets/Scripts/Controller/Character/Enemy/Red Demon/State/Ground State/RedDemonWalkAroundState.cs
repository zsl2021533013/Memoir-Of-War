﻿using Architecture;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Red_Demon.Core;
using Controller.Character.Enemy.Red_Demon.State.Base_State;
using Controller.Character.Enemy.Red_Demon.State.Skill_State;
using Controller.Character.Enemy.Red_Demon.State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Red_Demon.State.Ground_State
{
    public class RedDemonWalkAroundState : RedDemonState
    {
        public RedDemonWalkAroundState(string animationName) : base(animationName)
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
                    TranslateToState(controller.GetState<RedDemonTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<RedDemonTurnRightState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<RedDemonTurnRightState>());
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
                    TranslateToState(controller.GetState<RedDemonTurnLeftState>());
                    break;
                case < 225:
                    TranslateToState(controller.GetState<RedDemonTurnRightState>());
                    break;
                case < 315:
                    TranslateToState(controller.GetState<RedDemonTurnRightState>());
                    break;
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                var attackType = Random.Range(0, 1000);
                switch (attackType)
                {
                    case < 350:
                        TranslateToState(controller.GetState<RedDemonAttack1State>());
                        break;
                    case < 700:
                        TranslateToState(controller.GetState<RedDemonAttack2State>());
                        break;
                    default:
                        TranslateToState(controller.GetState<RedDemonAttack3State>());
                        break;
                }
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<RedDemonChaseState>());
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