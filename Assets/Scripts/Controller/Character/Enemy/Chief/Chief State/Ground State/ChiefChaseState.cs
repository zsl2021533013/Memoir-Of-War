using Controller.Character.Enemy.Chief.Chief_State.Base_State;
using Controller.Character.Enemy.Chief.Chief_State.Skill_State;
using Controller.Character.Enemy.Chief.Chief_State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Chief.Chief_State.Ground_State
{
    public class ChiefChaseState : ChiefState
    {
        public ChiefChaseState(string animationName) : base(animationName)
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
                    TranslateToState(controller.GetState<ChiefTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<ChiefTurnBackState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<ChiefTurnRightState>());
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
                    TranslateToState(controller.GetState<ChiefAttack1State>());
                }
                else
                {
                    TranslateToState(controller.GetState<ChiefAttack2State>());
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