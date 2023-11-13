using Controller.Character.Enemy.Enemy_Base.Controller;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Janissary.Core;
using Controller.Character.Enemy.Janissary.State.Ground_State;
using Controller.Character.Enemy.Janissary.State.Skill_State;
using Controller.Character.Enemy.Janissary.State.Stabbed_State;
using Controller.Character.Enemy.Janissary.State.Turn_State;
using Extension;
using UnityEngine.Animations.Rigging;

namespace Controller.Character.Enemy.Janissary.Controller
{
    public class JanissaryController : EnemyController
    {
        private EnemyWeaponController mShieldController;
        
        protected override EnemyState StartState => GetState<JanissaryIdleState>();

        protected override EnemyCore InitCore => new JanissaryCore();

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
            RegisterState(new JanissaryIdleState("Idle"));
            RegisterState(new JanissaryChaseState("Chase"));
            RegisterState(new JanissaryDeathState("Death"));
            RegisterState(new JanissaryWalkAroundState("Walk Around"));
            
            RegisterState(new JanissaryAttack1State("Attack 1"));
            RegisterState(new JanissaryAttack2State("Attack 2"));
            RegisterState(new JanissaryAttack3State("Attack 3"));
            RegisterState(new JanissaryAttack4State("Attack 4"));
            
            RegisterState(new JanissaryShieldBreakState("Shield Break"));
            RegisterState(new JanissaryStabbedDeathState("Stabbed Death"));
            RegisterState(new JanissaryStabbedRaiseState("Stabbed Raise"));
            RegisterState(new JanissaryStabbedState("Stabbed"));
            
            RegisterState(new JanissaryTurnLeftState("Turn Left"));
            RegisterState(new JanissaryTurnRightState("Turn Right"));
        }
        
        public void OpenShield() => mShieldController.OpenWeapon();
        public void CloseShield() => mShieldController.CloseWeapon();
        public void EnableShieldKnockDownAttack() => mShieldController.EnableKnockDownAttack(); // 动画调用
        public void EnableShieldKnockUpAttack() => mShieldController.EnableKnockUpAttack(); // 动画调用
    }
}