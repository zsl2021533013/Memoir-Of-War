using Controller.Character.Enemy.Warrior.Warrior_State.Base_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Skill_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Warrior.Warrior_State.Ground_State
{
    public class WarriorChaseState : WarriorState
    {
        public WarriorChaseState(string animationName) : base(animationName)
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
            
            if (core.HasArrived())
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
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.DisableNavMeshAgentRotation();
        }
    }
}