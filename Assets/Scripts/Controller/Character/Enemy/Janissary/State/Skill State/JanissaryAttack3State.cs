using Command.Particle_Effect;
using Controller.Character.Enemy.Janissary.Core;
using Controller.Character.Enemy.Janissary.State.Sub_State;
using DG.Tweening;
using QFramework;

namespace Controller.Character.Enemy.Janissary.State.Skill_State
{
    public class JanissaryAttack3State : JanissarySkillState
    {
        public JanissaryAttack3State(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            DOVirtual.DelayedCall(1f, (core as JanissaryCore).Beam);
            
            core.SendCommand<WarningOpenCommand>();
        }
    }
}