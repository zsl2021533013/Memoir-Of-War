using Controller.Character.Enemy.Warrior.Warrior_State.Base_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Ground_State;
using Controller.Character.Enemy.Warrior.Warrior_State.Stabbed_State;

namespace Controller.Character.Enemy.Warrior.Warrior_State.Sub_State
{
    public class WarriorSkillState : WarriorState
    {
        public WarriorSkillState(string animationName) : base(animationName)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            core.EnableNavMeshAgentRotation();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }
            
            if (core.IsShieldBreak)
            {
                TranslateToState(controller.GetState<WarriorShieldBreakState>());
            }
            

            if (core.IsAnimationEnd && core.HasArrived())
            {
                TranslateToState(controller.GetState<WarriorWalkAroundState>());
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<WarriorChaseState>());
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.DisableNavMeshAgentRotation();
        }
    }
}