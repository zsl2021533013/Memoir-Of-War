using Controller.Character.Enemy.Warrior.Warrior_State.Base_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Ground_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Skill_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Warrior.Warrior_State.Sub_State
{
    public class WarriorTurnState : WarriorState
    {
        public WarriorTurnState(string animationName) : base(animationName)
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
                        TranslateToState(controller.GetState<WarriorTurnLeftState>());
                        break;
                    case < 225:
                        TranslateToState(controller.GetState<WarriorTurnBackState>());
                        break;
                    case < 315:
                        TranslateToState(controller.GetState<WarriorTurnRightState>());
                        break;
                }
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
    }
}