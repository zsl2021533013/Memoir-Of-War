using Controller.Character.Enemy.Red_Demon.Core;
using Controller.Character.Enemy.Red_Demon.State.Base_State;
using Controller.Character.Enemy.Red_Demon.State.Skill_State;
using Controller.Character.Enemy.Red_Demon.State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Red_Demon.State.Ground_State
{
    public class RedDemonChaseState : RedDemonState
    {
        private enum ChaseType
        {
            Rock_Shoot,
            Normal
        }
        
        private ChaseType mType;
        
        public RedDemonChaseState(string animationName) : base(animationName)
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
                    TranslateToState(controller.GetState<RedDemonTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<RedDemonTurnRightState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<RedDemonTurnRightState>());
                    return;
            }
            
            core.EnableNavMeshAgentRotation();
            
            mType = Random.Range(0, 1000) < 500 ? ChaseType.Rock_Shoot : ChaseType.Normal;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if ((core as RedDemonCore).HasArrivedAttack4Range() && mType == ChaseType.Rock_Shoot)
            {
                TranslateToState(controller.GetState<RedDemonAttack4State>());
            }
            
            if (core.HasArrived())
            {
                var attackType = Random.Range(0, 1000);
                switch (attackType)
                {
                    case < 350:
                        TranslateToState(controller.GetState<RedDemonAttack1State>());
                        break;
                    case < 700:
                        TranslateToState(controller.GetState<RedDemonAttack2State>());
                        break;
                    default:
                        TranslateToState(controller.GetState<RedDemonAttack3State>());
                        break;
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