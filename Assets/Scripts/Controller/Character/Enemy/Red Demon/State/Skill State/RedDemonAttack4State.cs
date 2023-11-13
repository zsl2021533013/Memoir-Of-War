using Controller.Character.Enemy.Red_Demon.Core;
using Controller.Character.Enemy.Red_Demon.State.Base_State;
using Controller.Character.Enemy.Red_Demon.State.Sub_State;
using DG.Tweening;

namespace Controller.Character.Enemy.Red_Demon.State.Skill_State
{
    public class RedDemonAttack4State : RedDemonSkillState
    {
        public RedDemonAttack4State(string animationName) : base(animationName)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            DOVirtual.DelayedCall(1f, () => (core as RedDemonCore)?.RockShoot());
        }
    }
}