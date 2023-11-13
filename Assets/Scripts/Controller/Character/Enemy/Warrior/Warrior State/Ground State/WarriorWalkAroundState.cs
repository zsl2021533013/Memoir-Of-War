using Architecture;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Warrior.Core;
using Controller.Character.Enemy.Warrior.Warrior_State.Base_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Skill_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Turn_State;
using UnityEngine;
using UnityEngine.AI;

namespace Controller.Character.Enemy.Warrior.Warrior_State.Ground_State
{
    public class WarriorWalkAroundState : WarriorState
    {
        public WarriorWalkAroundState(string animationName) : base(animationName)
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
                    TranslateToState(controller.GetState<WarriorTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<WarriorTurnBackState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<WarriorTurnRightState>());
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
                    TranslateToState(controller.GetState<WarriorTurnLeftState>());
                    break;
                case < 225:
                    TranslateToState(controller.GetState<WarriorTurnBackState>());
                    break;
                case < 315:
                    TranslateToState(controller.GetState<WarriorTurnRightState>());
                    break;
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                var attackType = Random.Range(0, 1000);
                if (attackType < 500)
                {
                    TranslateToState(controller.GetState<WarriorAttack1State>());
                }
                else
                {
                    TranslateToState(controller.GetState<WarriorAttack2State>());
                }
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<WarriorChaseState>());
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