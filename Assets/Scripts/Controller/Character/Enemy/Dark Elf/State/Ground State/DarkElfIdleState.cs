using Architecture;
using Controller.Character.Enemy.Dark_Elf.State.Base_State;
using Controller.Character.Enemy.Dark_Elf.State.Skill_State;
using UnityEngine;

namespace Controller.Character.Enemy.Dark_Elf.State.Ground_State
{
    public class DarkElfIdleState : DarkElfState
    {
        public DarkElfIdleState(string animationName) : base(animationName)
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

            if (core.IsAnimationEnd)
            {
                var attackType = Random.Range(0, 1000);
                switch (attackType)
                {
                    case < 300:
                        TranslateToState(controller.GetState<DarkElfAttack1State>());
                        break;
                    case < 600:
                        TranslateToState(controller.GetState<DarkElfAttack2State>());
                        break;
                    default:
                        TranslateToState(controller.GetState<DarkElfAttack3State>());
                        break;
                }
            }
        }
    }
}