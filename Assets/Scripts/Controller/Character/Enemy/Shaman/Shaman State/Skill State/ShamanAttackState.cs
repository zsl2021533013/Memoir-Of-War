using Controller.Character.Enemy.Shaman.Core;
using Controller.Character.Enemy.Shaman.Shaman_State.Sub_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Turn_State;
using DG.Tweening;

namespace Controller.Character.Enemy.Shaman.Shaman_State.Skill_State
{
    public class ShamanAttackState : ShamanSkillState
    {
        public ShamanAttackState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            switch (core.GetPlayerAngle())
            {
                case < 45:
                    break;
                case < 135:
                    TranslateToState(controller.GetState<ShamanTurnLeftState>());
                    return;
                case < 225:
                    TranslateToState(controller.GetState<ShamanTurnBackState>());
                    return;
                case < 315:
                    TranslateToState(controller.GetState<ShamanTurnRightState>());
                    return;
            }
            
            base.OnEnter();
        }
    }
}