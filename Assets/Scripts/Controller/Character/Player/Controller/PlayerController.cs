using System.Input_System;
using Architecture;
using Controller.Character.Player.Core;
using Controller.Character.Player.FSM.State_Machine;
using Controller.Character.Player.Player_State;
using Controller.Character.Player.Player_State.Player_Base_State;
using Controller.Character.Player.Player_State.Player_Ground_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Attack_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Doge_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Knock_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Parry_State;
using DG.Tweening;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Controller.Character.Player.Controller
{
    public class PlayerController : MonoBehaviour, IController
    {
        private Rigidbody mRb;
        private Animator mAnimator;
        private PlayerCore mCore;
        private PlayerStateMachine mStateMachine = new PlayerStateMachine();
        private IOCContainer mStateContainer = new IOCContainer();
        private PlayerWeaponController mWeaponController;
        private Cinemachine.CinemachineImpulseSource mImpulse;
        
        private bool mApplyRootMotion = true;
        private bool mIsPlayerConstrained = false; 
        /*
         * 设置一个 mIsPlayerConstrained 是因为玩家状态内部会对 mApplyRootMotion 进行操作
         * 而有些组件（如 PlayerConstrainCommand）则需要无视这种操作，以更高的权限来决定是否开闭根运动
         * 故加入一个 mIsPlayerConstrained 联合作用
         */

        private bool ApplyRootMotion => mApplyRootMotion && !mIsPlayerConstrained;

        private void Awake()
        {
            InitializeComponent();
            InitializeFSM();

            this.GetModel<IPlayerModel>().RegisterPlayer(transform);
        }

        private void Start()
        {
            mStateMachine.Initialize(GetState<PlayerGroundState>());
        }

        private void Update()
        {
            mStateMachine.OnUpdate();
        }

        private void FixedUpdate()
        {
            mStateMachine.OnFixedUpdate();
        }

        private void OnAnimatorMove()
        {
            if (!ApplyRootMotion)
            {
                return;
            }
            
            var velocity = mAnimator.velocity;
            velocity.y = mRb.velocity.y;
            mRb.velocity = velocity;
        }

        private void InitializeFSM()
        {
            RegisterState(new PlayerGroundState("Ground"));
            RegisterState(new PlayerMoveForwardState("Timeline Move"));
            RegisterState(new PlayerAttack1State("Attack 1"));
            RegisterState(new PlayerAttack2State("Attack 2"));
            RegisterState(new PlayerAttack3State("Attack 3"));
            RegisterState(new PlayerAttack4State("Attack 4"));
            RegisterState(new PlayerDefenceState("Defence"));
            RegisterState(new PlayerDodgeLeftState("Dodge Left"));
            RegisterState(new PlayerDodgeRightState("Dodge Right"));
            RegisterState(new PlayerKnockUpState("Knock Up"));
            RegisterState(new PlayerKnockDownState("Knock Down"));
            RegisterState(new PlayerHurtState("Hurt"));
            RegisterState(new PlayerStabState("Stab"));
            RegisterState(new PlayerDeathState("Death"));
        }

        private void InitializeComponent()
        {
            mRb = GetComponent<Rigidbody>();
            mAnimator = GetComponent<Animator>();
            mWeaponController = GetComponentInChildren<PlayerWeaponController>().CloseWeapon();
            mImpulse = GetComponent<Cinemachine.CinemachineImpulseSource>();
            
            RegisterCore(new PlayerCore());
        }

        private void RegisterState<TState>(TState state) where TState : PlayerState
        {
            state.SetController(this)
                .SetCoreManager(mCore)
                .SetStateMachine(mStateMachine);
            
            mStateContainer.Register<TState>(state);
        }
        
        public TState GetState<TState>() where TState : PlayerState
        {
            return mStateContainer.Get<TState>();
        }

        private PlayerController RegisterCore(PlayerCore core)
        {
            core.InitCore(transform);
            mCore = core;
            return this;
        }

        public PlayerController AddForce(float velocity)
        {
            DOTween.Sequence()
                .AppendCallback(() => DisableRootMotion())
                .AppendCallback(() => StartControl())
                .AppendInterval(MemoirOfWarAsset.PlayerKnockBackTime)
                .AppendCallback(() => EnableRootMotion())
                .AppendCallback(() => EndControl());
            mRb.velocity = Vector3.zero;
            mRb.AddForce(-transform.forward * velocity, ForceMode.Impulse);
            return this;
        }

        public PlayerController Impulse()
        { 
            mImpulse.GenerateImpulse();
            return this;
        }

        #region External Input

        public PlayerController AnimationEnd() // 动画调用
        {
            mCore.AnimationEnd();
            CloseWeapon();
            return this;
        }

        public PlayerController OpenWeapon()
        {
            mWeaponController.OpenWeapon();
            return this;
        }

        private PlayerController CloseWeapon()
        {
            mWeaponController.CloseWeapon();
            return this;
        }

        public PlayerController EnableRootMotion()
        {
            mApplyRootMotion = true;
            return this;
        }

        public PlayerController DisableRootMotion()
        {
            mApplyRootMotion = false;
            return this;
        }

        public PlayerController ConstrainPlayer()
        {
            mIsPlayerConstrained = true;
            return this;
        }
        
        public PlayerController FreePlayer()
        {
            mIsPlayerConstrained = false;
            return this;
        }
        
        public bool CheckPlayerState<TState>() where TState : PlayerState
        {
            return (mStateMachine.CurrentState is TState) && !mCore.IsAnimationEnd;
        }
        
        public PlayerController StartControl()
        {
            mCore.StartControl();
            return this;
        }
        
        public PlayerController EndControl()
        {
            mCore.EndControl();
            return this;
        }
        
        public PlayerController KnockDown()
        {
            mCore.KnockDown();
            return this;
        }

        public PlayerController KnockUp()
        {
            mCore.KnockUp();
            return this;
        }

        public PlayerController GetHit()
        {
            mCore.Hurt();
            return this;
        }

        public PlayerController Die()
        {
            mCore.Die();
            return this;
        }
        
        public PlayerController EnableTimeline() // 必须设置为 void 才能被 Signal Receiver 使用
        {
            mCore.EnableTimeline();
            return this;
        }
        
        public PlayerController DisableTimeline()
        { 
            mCore.DisableTimeline();
            return this;
        }

        public PlayerController ResetInput()
        {
            InputKit.Instance.ResetInput();
            return this;
        }

        private void OnDrawGizmosSelected()
        {
            mCore?.OnDrawGizmosSelected();
        }

        #endregion

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}