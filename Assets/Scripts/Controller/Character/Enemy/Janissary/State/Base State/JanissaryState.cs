using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Janissary.Controller;
using Controller.Character.Enemy.Janissary.State.Ground_State;

namespace Controller.Character.Enemy.Janissary.State.Base_State
{
    public class JanissaryState : EnemyState
    {
        public JanissaryState(string animationName) : base(animationName)
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
                TranslateToState(controller.GetState<JanissaryDeathState>());
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            
            ((JanissaryController)controller).CloseShield();
        }
    }
}