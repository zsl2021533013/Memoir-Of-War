using Architecture;
using Controller.Character.Enemy.Janissary.Core;
using Controller.Character.Enemy.Janissary.State.Base_State;
using UnityEngine;

namespace Controller.Character.Enemy.Janissary.State.Ground_State
{
    public class JanissaryIdleState : JanissaryState
    {
        public JanissaryIdleState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<JanissaryWalkAroundState>());
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<JanissaryChaseState>());
            }
        }
    }
}