using Controller.Character.Enemy.Shaman.Shaman_State.Base_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Skill_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Shaman.Shaman_State.Ground_State
{
    public class ShamanChaseState : ShamanState
    {
        public ShamanChaseState(string animationName) : base(animationName)
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
                    TranslateToState(controller.GetState<ShamanTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<ShamanTurnBackState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<ShamanTurnRightState>());
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
                TranslateToState(controller.GetState<ShamanAttackState>());
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.DisableNavMeshAgentRotation();
        }
    }
}