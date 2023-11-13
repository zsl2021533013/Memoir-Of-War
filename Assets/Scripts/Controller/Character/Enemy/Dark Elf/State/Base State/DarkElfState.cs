using Controller.Character.Enemy.Dark_Elf.State.Ground_State;
using Controller.Character.Enemy.Enemy_Base.State;

namespace Controller.Character.Enemy.Dark_Elf.State.Base_State
{
    public class DarkElfState : EnemyState
    {
        public DarkElfState(string animationName) : base(animationName)
        {
        }
        
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isStateOver)
            {
                return;
            }

            if (core.IsDead)
            {
                core.ResetDeath();
                TranslateToState(controller.GetState<DarkElfDeathState>());
            }
        }
    }
}