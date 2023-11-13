using Controller.Character.Enemy.Enemy_Base.Controller;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Undead_Knight.Core;
using Controller.Character.Enemy.Undead_Knight.State.Ground_State;
using Controller.Character.Enemy.Undead_Knight.State.Skill_State;
using Controller.Character.Enemy.Undead_Knight.State.Stabbed_State;
using Controller.Character.Enemy.Undead_Knight.State.Turn_State;
using Extension;

namespace Controller.Character.Enemy.Undead_Knight.Controller
{
    public class UndeadKnightController : EnemyController
    {
        private EnemyWeaponController mShieldController;
        protected override EnemyState StartState => GetState<UndeadKnightIdleState>();
        protected override EnemyCore InitCore => new UndeadKnightCore();

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            
            mShieldController = transform.FindAllChildren("Shield")
                .GetComponent<EnemyWeaponController>()
                .CloseWeapon()
                .SetRootTransform(transform);
        }

        protected override void InitializeFSM()
        {
            RegisterState(new UndeadKnightIdleState("Idle"));
            RegisterState(new UndeadKnightChaseState("Chase"));
            RegisterState(new UndeadKnightDeathState("Death"));
            RegisterState(new UndeadKnightWalkAroundState("Walk Around"));
            
            RegisterState(new UndeadKnightAttack1State("Attack 1"));
            RegisterState(new UndeadKnightAttack2State("Attack 2"));
            RegisterState(new UndeadKnightAttack3State("Attack 3"));
            RegisterState(new UndeadKnightAttack4State("Attack 4"));
            
            RegisterState(new UndeadKnightShieldBreakState("Shield Break"));
            RegisterState(new UndeadKnightStabbedDeathState("Stabbed Death"));
            RegisterState(new UndeadKnightStabbedRaiseState("Stabbed Raise"));
            RegisterState(new UndeadKnightStabbedState("Stabbed"));
            
            RegisterState(new UndeadKnightTurnLeftState("Turn Left"));
            RegisterState(new UndeadKnightTurnRightState("Turn Right"));
            RegisterState(new UndeadKnightTurnBackState("Turn Back"));
        }

        public void OpenShield() => mShieldController.OpenWeapon();
        public void CloseShield() => mShieldController.CloseWeapon();
        public void EnableShieldKnockDownAttack() => mShieldController.EnableKnockDownAttack(); // 动画调用
        public void EnableShieldKnockUpAttack() => mShieldController.EnableKnockUpAttack(); // 动画调用
    }
}