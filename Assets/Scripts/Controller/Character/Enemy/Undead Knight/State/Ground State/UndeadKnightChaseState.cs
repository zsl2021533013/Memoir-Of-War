using Controller.Character.Enemy.Undead_Knight.Core;
using Controller.Character.Enemy.Undead_Knight.State.Base_State;
using Controller.Character.Enemy.Undead_Knight.State.Skill_State;
using Controller.Character.Enemy.Undead_Knight.State.Turn_State;
using UnityEngine;

namespace Controller.Character.Enemy.Undead_Knight.State.Ground_State
{
    public class UndeadKnightChaseState : UndeadKnightState
    {
        private enum ChaseType
        {
            Slash,
            Normal
        }

        private ChaseType mType;
        
        public UndeadKnightChaseState(string animationName) : base(animationName)
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
                    TranslateToState(controller.GetState<UndeadKnightTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<UndeadKnightTurnBackState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<UndeadKnightTurnRightState>());
                    return;
            }
            
            core.EnableNavMeshAgentRotation();
            
            mType = Random.Range(0, 1000) < 500 ? ChaseType.Slash : ChaseType.Normal;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if ((core as UndeadKnightCore).HasArrivedAttack4Range() && mType == ChaseType.Slash)
            {
                TranslateToState(controller.GetState<UndeadKnightAttack4State>());
            }
            
            if (core.HasArrived())
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
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.DisableNavMeshAgentRotation();
        }
    }
}