using Controller.Character.Enemy.Chief.Chief_State.Base_State;
using Controller.Character.Enemy.Chief.Chief_State.Ground_State;
using Controller.Character.Enemy.Chief.Chief_State.Skill_State;
using Controller.Character.Enemy.Chief.Chief_State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Chief.Chief_State.Sub_State
{
    public class ChiefTurnState : ChiefState
    {
        public ChiefTurnState(string animationName) : base(animationName)
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
                        TranslateToState(controller.GetState<ChiefTurnLeftState>());
                        break;
                    case < 225:
                        TranslateToState(controller.GetState<ChiefTurnBackState>());
                        break;
                    case < 315:
                        TranslateToState(controller.GetState<ChiefTurnRightState>());
                        break;
                }
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                var attackType = Random.Range(0, 1000);
                if (attackType < 500)
                {
                    TranslateToState(controller.GetState<ChiefAttack1State>());
                }
                else
                {
                    TranslateToState(controller.GetState<ChiefAttack2State>());
                }
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<ChiefChaseState>());
            }
        }
    }
}