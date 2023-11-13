using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Undead_Knight.State.Ground_State;

namespace Controller.Character.Enemy.Undead_Knight.State.Base_State
{
    public class UndeadKnightState : EnemyState
    {
        public UndeadKnightState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<UndeadKnightDeathState>());
            }
        }
    }
}