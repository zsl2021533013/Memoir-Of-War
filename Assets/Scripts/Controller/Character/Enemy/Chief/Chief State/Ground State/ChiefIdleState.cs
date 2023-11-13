using Architecture;
using Controller.Character.Enemy.Chief.Chief_State.Base_State;
using Controller.Character.Enemy.Chief.Chief_State.Skill_State;
using UnityEngine;

namespace Controller.Character.Enemy.Chief.Chief_State.Ground_State
{
    public class ChiefIdleState : ChiefState
    {
        public ChiefIdleState(string animationName) : base(animationName)
        {
        }
        
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (Time.time > startTime + MemoirOfWarAsset.IdleTime)
            {
                core.AnimationEnd();
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