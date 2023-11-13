using Architecture;
using Controller.Character.Enemy.Red_Demon.State.Base_State;
using UnityEngine;

namespace Controller.Character.Enemy.Red_Demon.State.Ground_State
{
    public class RedDemonIdleState : RedDemonState
    {
        public RedDemonIdleState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<RedDemonWalkAroundState>());
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<RedDemonChaseState>());
            }
        }
    }
}