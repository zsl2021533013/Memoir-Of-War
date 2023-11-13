using Controller.Character.Enemy.Enemy_Base.Controller;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Shaman.Core;
using Controller.Character.Enemy.Shaman.Shaman_State.Ground_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Skill_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Stabbed_State;
using Controller.Character.Enemy.Shaman.Shaman_State.Turn_State;

namespace Controller.Character.Enemy.Shaman.Controller
{
    public class ShamanController : EnemyController 
    {
        protected override EnemyState StartState => GetState<ShamanIdleState>();
        protected override EnemyCore InitCore => new ShamanCore();

        protected override void InitializeFSM()
        {
            RegisterState(new ShamanIdleState("Idle"));
            RegisterState(new ShamanChaseState("Chase"));
            RegisterState(new ShamanDeathState("Death"));
            RegisterState(new ShamanWalkAroundState("Walk Around"));
            RegisterState(new ShamanWalkBackState("Walk Back"));
            
            RegisterState(new ShamanAttackState("Attack"));
            
            RegisterState(new ShamanShieldBreakState("Shield Break"));
            RegisterState(new ShamanStabbedDeathSate("Stabbed Death"));
            RegisterState(new ShamanStabbedRaiseState("Stabbed Raise"));
            RegisterState(new ShamanStabbedState("Stabbed"));
            
            RegisterState(new ShamanTurnLeftState("Turn Left"));
            RegisterState(new ShamanTurnRightState("Turn Right"));
            RegisterState(new ShamanTurnBackState("Turn Back"));
        }

        // 动画调用
        public ShamanController ShootFireball()
        {
            ((ShamanCore)mCore).ShootFireball();
            return this;
        }
    }
}
