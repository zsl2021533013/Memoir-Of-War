using Controller.Character.Enemy.Undead_Knight.State.Base_State;
using Controller.Character.Enemy.Undead_Knight.State.Ground_State;
using Controller.Character.Enemy.Undead_Knight.State.Skill_State;
using Controller.Character.Enemy.Undead_Knight.State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Undead_Knight.State.Sub_State
{
    public class UndeadKnightTurnState : UndeadKnightState
    {
        public UndeadKnightTurnState(string animationName) : base(animationName)
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
                        TranslateToState(controller.GetState<UndeadKnightTurnLeftState>());
                        break;
                    case < 225:
                        TranslateToState(controller.GetState<UndeadKnightTurnBackState>());
                        break;
                    case < 315:
                        TranslateToState(controller.GetState<UndeadKnightTurnRightState>());
                        break;
                }
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
    }
}