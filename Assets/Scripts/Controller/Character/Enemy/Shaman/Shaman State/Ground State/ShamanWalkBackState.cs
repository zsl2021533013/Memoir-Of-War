using Architecture;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Shaman.Core;
using Controller.Character.Enemy.Shaman.Shaman_State.Base_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Skill_State;
using UnityEngine;

namespace Controller.Character.Enemy.Shaman.Shaman_State.Ground_State
{
    public class ShamanWalkBackState : ShamanState
    {
        public ShamanWalkBackState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (!((ShamanCore)core).DetectCanWalkAround())
            {
                core.SetWalkAroundType(WalkAroundType.Idle);
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
            
            if (!((ShamanCore)core).DetectCanWalkAround())
            {
                core.SetWalkAroundType(WalkAroundType.Idle);
            }

            if (Time.time > startTime + MemoirOfWarAsset.IdleTime)
            {
                core.AnimationEnd();
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
        
        public override void OnExit()
        {
            base.OnExit();
            
            var type = Random.Range(0, 1000);
            switch (type)
            {
                case < 500:
                    core.SetWalkAroundType(WalkAroundType.Left);
                    break;
                default:
                    core.SetWalkAroundType(WalkAroundType.Right);
                    break;
            }
            
            core.DisableNavMeshAgentRotation();
        }
    }
}