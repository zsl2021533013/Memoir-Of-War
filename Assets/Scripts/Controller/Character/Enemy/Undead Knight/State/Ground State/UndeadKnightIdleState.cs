using Architecture;
using Controller.Character.Enemy.Undead_Knight.State.Base_State;
using UnityEngine;

namespace Controller.Character.Enemy.Undead_Knight.State.Ground_State
{
    public class UndeadKnightIdleState : UndeadKnightState
    {
        public UndeadKnightIdleState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<UndeadKnightWalkAroundState>());
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<UndeadKnightChaseState>());
            }
        }
    }
}