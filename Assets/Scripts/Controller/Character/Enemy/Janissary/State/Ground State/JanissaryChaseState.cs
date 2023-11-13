using Controller.Character.Enemy.Janissary.Core;
using Controller.Character.Enemy.Janissary.State.Base_State;
using Controller.Character.Enemy.Janissary.State.Skill_State;
using Controller.Character.Enemy.Janissary.State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Janissary.State.Ground_State
{
    public class JanissaryChaseState : JanissaryState
    {
        private enum ChaseType
        {
            ShieldDash,
            Normal
        }
        
        private ChaseType mType;
        
        public JanissaryChaseState(string animationName) : base(animationName)
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
                    TranslateToState(controller.GetState<JanissaryTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<JanissaryTurnRightState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<JanissaryTurnRightState>());
                    return;
            }
            
            core.EnableNavMeshAgentRotation();
            
            mType = Random.Range(0, 1000) < 500 ? ChaseType.ShieldDash : ChaseType.Normal;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if ((core as JanissaryCore).HasArrivedAttack4Range() && mType == ChaseType.ShieldDash)
            {
                TranslateToState(controller.GetState<JanissaryAttack4State>());
            }
            
            if (core.HasArrived())
            {
                var attackType = Random.Range(0, 900);
                switch (attackType)
                {
                    case < 300:
                        TranslateToState(controller.GetState<JanissaryAttack1State>());
                        break;
                    case < 600:
                        TranslateToState(controller.GetState<JanissaryAttack2State>());
                        break;
                    default:
                        TranslateToState(controller.GetState<JanissaryAttack3State>());
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