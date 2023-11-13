using Controller.Character.Enemy.Shaman.Core;
using Controller.Character.Enemy.Shaman.Shaman_State.Base_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Ground_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Skill_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Shaman.Shaman_State.Sub_State
{
    public class ShamanTurnState : ShamanState
    {
        public ShamanTurnState(string animationName) : base(animationName)
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
                        TranslateToState(controller.GetState<ShamanTurnLeftState>());
                        break;
                    case < 225:
                        TranslateToState(controller.GetState<ShamanTurnBackState>());
                        break;
                    case < 315:
                        TranslateToState(controller.GetState<ShamanTurnRightState>());
                        break;
                }
            }
            
            if ((core as ShamanCore).TooClose() && ((ShamanCore)core).DetectCanWalkAround())
            {
                TranslateToState(controller.GetState<ShamanWalkBackState>());
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                TranslateToState(controller.GetState<ShamanAttackState>());
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<ShamanChaseState>());
            }
        }
    }
}