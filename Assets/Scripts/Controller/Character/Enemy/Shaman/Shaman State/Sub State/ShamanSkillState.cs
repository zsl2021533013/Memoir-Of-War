using Controller.Character.Enemy.Shaman.Core;
using Controller.Character.Enemy.Shaman.Shaman_State.Base_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Ground_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Stabbed_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Turn_State;

namespace Controller.Character.Enemy.Shaman.Shaman_State.Sub_State
{
    public class ShamanSkillState : ShamanState
    {
        public ShamanSkillState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<ShamanShieldBreakState>());
            }

            if (core.IsAnimationEnd && core.HasArrived())
            {
                TranslateToState(controller.GetState<ShamanWalkAroundState>());
            }
            
            if (core.IsAnimationEnd && !core.HasArrived()) 
            {
                TranslateToState(controller.GetState<ShamanChaseState>());
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            
            core.DisableNavMeshAgentRotation();
        }
    }
}