using Controller.Character.Enemy.Janissary.State.Base_State;
using Controller.Character.Enemy.Janissary.State.Ground_State;
using Controller.Character.Enemy.Janissary.State.Skill_State;
using Controller.Character.Enemy.Janissary.State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Janissary.State.Sub_State
{
    public class JanissaryTurnState : JanissaryState
    {
        public JanissaryTurnState(string animationName) : base(animationName)
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
                        TranslateToState(controller.GetState<JanissaryTurnLeftState>());
                        break;
                    case < 225:
                        TranslateToState(controller.GetState<JanissaryTurnRightState>());
                        break;
                    case < 315:
                        TranslateToState(controller.GetState<JanissaryTurnRightState>());
                        break;
                }
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                var attackType = Random.Range(0, 1000);
                switch (attackType)
                {
                    case < 350:
                        TranslateToState(controller.GetState<JanissaryAttack1State>());
                        break;
                    case < 700:
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
    }
}