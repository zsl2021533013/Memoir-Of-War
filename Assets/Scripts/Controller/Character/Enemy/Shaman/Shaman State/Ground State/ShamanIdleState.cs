using Architecture;
using Controller.Character.Enemy.Shaman.Core;
using Controller.Character.Enemy.Shaman.Shaman_State.Base_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Skill_State;
using UnityEngine;

namespace Controller.Character.Enemy.Shaman.Shaman_State.Ground_State
{
    public class ShamanIdleState : ShamanState
    {
        public ShamanIdleState(string animationName) : base(animationName)
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
            
            if (core.IsAnimationEnd && ((ShamanCore)core).TooClose())
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