using Controller.Character.Enemy.Dark_Elf.State.Base_State;
using Controller.Character.Enemy.Dark_Elf.State.Ground_State;
using Controller.Character.Enemy.Dark_Elf.State.Skill_State;
using Controller.Character.Enemy.Dark_Elf.State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Dark_Elf.State.Sub_State
{
    public class DarkElfTurnState : DarkElfState
    {
        public DarkElfTurnState(string animationName) : base(animationName)
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
                        TranslateToState(controller.GetState<DarkElfTurnLeftState>());
                        break;
                    case < 225:
                        TranslateToState(controller.GetState<DarkElfTurnBackState>());
                        break;
                    case < 315:
                        TranslateToState(controller.GetState<DarkElfTurnRightState>());
                        break;
                }
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
    }
}