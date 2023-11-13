using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Shaman.Shaman_State.Ground_State;

namespace Controller.Character.Enemy.Shaman.Shaman_State.Base_State
{
    public class ShamanState : EnemyState
    {
        public ShamanState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<ShamanDeathState>());
            }
        }
    }
}