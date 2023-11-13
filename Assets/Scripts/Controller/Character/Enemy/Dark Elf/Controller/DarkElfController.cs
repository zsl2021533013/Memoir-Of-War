using Command.Particle_Effect;
using Controller.Character.Enemy.Dark_Elf.Core;
using Controller.Character.Enemy.Dark_Elf.State.Ground_State;
using Controller.Character.Enemy.Dark_Elf.State.Skill_State;
using Controller.Character.Enemy.Dark_Elf.State.Stabbed_State;
using Controller.Character.Enemy.Dark_Elf.State.Turn_State;
using Controller.Character.Enemy.Enemy_Base.Controller;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Enemy_Base.State;
using Extension;
using QFramework;

namespace Controller.Character.Enemy.Dark_Elf.Controller
{
    public class DarkElfController : EnemyController
    {
        protected override EnemyState StartState => GetState<DarkElfIdleState>();
        protected override EnemyCore InitCore => new DarkElfCore();

        protected override void InitializeFSM()
        {
            RegisterState(new DarkElfIdleState("Idle"));
            RegisterState(new DarkElfDeathState("Death"));
            RegisterState(new DarkElfWalkAroundState("Walk Around"));
            
            RegisterState(new DarkElfAttack1State("Attack 1"));
            RegisterState(new DarkElfAttack2State("Attack 2"));
            RegisterState(new DarkElfAttack3State("Attack 3"));
            
            RegisterState(new DarkElfShieldBreakState("Shield Break"));
            RegisterState(new DarkElfStabbedDeathState("Stabbed Death"));
            RegisterState(new DarkElfStabbedRaiseState("Stabbed Raise"));
            RegisterState(new DarkElfStabbedState("Stabbed"));
            
            RegisterState(new DarkElfTurnLeftState("Turn Left"));
            RegisterState(new DarkElfTurnRightState("Turn Right"));
            RegisterState(new DarkElfTurnBackState("Turn Back"));
        }
        
        // 动画调用
        public DarkElfController ShootFireball()
        {
            ((DarkElfCore)mCore).ShootFireball();
            return this;
        }
        
        public DarkElfController ShootFireballFromHighPosition()
        {
            this.SendCommand(new EnemyFireballSpawnCommand(transform,
                transform.position + 0.5f * transform.forward + 1.875f * transform.up));
            // 偏移量写的比较死
            return this;
        }
    }
}