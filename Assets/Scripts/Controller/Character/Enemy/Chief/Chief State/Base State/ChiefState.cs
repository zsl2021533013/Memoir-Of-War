using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Chief.Chief_State.Ground_State;

namespace Controller.Character.Enemy.Chief.Chief_State.Base_State
{
    public class ChiefState : EnemyState
    {
        public ChiefState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<ChiefDeathState>());
            }
        }
    }
}