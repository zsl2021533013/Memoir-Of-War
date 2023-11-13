using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Red_Demon.State.Ground_State;

namespace Controller.Character.Enemy.Red_Demon.State.Base_State
{
    public class RedDemonState : EnemyState
    {
        public RedDemonState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<RedDemonDeathState>());
            }
        }
    }
}