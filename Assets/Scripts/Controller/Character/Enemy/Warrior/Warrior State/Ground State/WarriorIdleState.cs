using Architecture;
using Controller.Character.Enemy.Warrior.Warrior_State.Base_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Skill_State;
using UnityEngine;

namespace Controller.Character.Enemy.Warrior.Warrior_State.Ground_State
{
    public class WarriorIdleState : WarriorState
    {
        public WarriorIdleState(string animationName) : base(animationName)
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
                    TranslateToState(controller.GetState<WarriorAttack1State>());
                }
                else
                {
                    TranslateToState(controller.GetState<WarriorAttack2State>());
                }
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<WarriorChaseState>());
            }
        }
    }
}