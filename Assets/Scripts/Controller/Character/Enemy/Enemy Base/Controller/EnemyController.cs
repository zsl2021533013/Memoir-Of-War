using System.Collections.Generic;
using Architecture;
using Command.Particle_Effect;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Enemy_Base.FSM;
using Controller.Character.Enemy.Enemy_Base.State;
using DG.Tweening;
using Extension;
using Model;
using Model.Player;
using QFramework;
using UnityEngine;
using UnityEngine.AI;

namespace Controller.Character.Enemy.Enemy_Base.Controller
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CapsuleCollider))]
    public abstract class EnemyController : MonoBehaviour, IController
    {
        protected Animator mAnimator;
        protected NavMeshAgent mAgent; 
        private List<Material> mMaterials = new List<Material>();
        // 这三个组件必须由 Controller 拥有，与整体生命周期，敌人移动有关。Material 则与生成有关，其余组件放于 Core
    
        protected EnemyCore mCore;
        private EnemyStateMachine mStateMachine = new EnemyStateMachine();
        private IOCContainer mStateContainer = new IOCContainer();
        private EnemyWeaponController mWeaponController;
        
        public bool IsShieldBreak => mCore.IsShieldBreak;

        protected abstract EnemyState StartState { get; } // 必须指明初始类
        protected abstract EnemyCore InitCore { get; }  // 必须指明初始类
        protected abstract void InitializeFSM(); // 必须初始化类
        
        public void Init()
        {
            InitializeComponent();
            
            InitializeFSM();

            mStateMachine.Init(StartState);
        }

        private void Update()
        {
            mStateMachine.OnUpdate();
        }

        private void FixedUpdate()
        {
            mStateMachine.OnFixedUpdate();
        }

        protected virtual void OnAnimatorMove()
        {
            Vector3 position = mAnimator.rootPosition;
            mAgent.nextPosition = new Vector3(position.x, mAgent.nextPosition.y, position.z);
            position.y = mAgent.nextPosition.y;
            transform.position = position;
            
            Quaternion rotation = mAnimator.rootRotation;
            transform.rotation = rotation;
        }

        protected virtual void InitializeComponent()
        {
            mAnimator = GetComponent<Animator>();
            mAgent = GetComponent<NavMeshAgent>();
            
            var renders = GetComponentsInChildren<Renderer>();
            foreach (var render in renders)
            {
                mMaterials.AddRange(render.materials);
            }
            
            mWeaponController = transform.FindAllChildren("Weapon")
                .GetComponentInChildren<EnemyWeaponController>()
                .CloseWeapon()
                .SetRootTransform(transform);

            #region NavMeshAgent

            mAgent.updatePosition = false;
            mAgent.updateRotation = false;
            mAgent.angularSpeed = MemoirOfWarAsset.AngleSpeed;

            #endregion
            
            RegisterCore(InitCore);
        }

        protected void RegisterState<TState>(TState state) where TState : EnemyState
        {
            state.SetController(this)
                .SetCoreManager(mCore)
                .SetStateMachine(mStateMachine);
            
            mStateContainer.Register<TState>(state);
        }
        
        public TState GetState<TState>() where TState : EnemyState
        {
            return mStateContainer.Get<TState>();
        }

        private void RegisterCore(EnemyCore core)
        {
            core.InitCore(transform);
            mCore = core;
        }

        #region Weapon Controller

        public void OpenWeapon() => mWeaponController.OpenWeapon(); // 动画调用
        public void CloseWeapon() => mWeaponController.CloseWeapon(); // 动画调用
        public void EnableKnockDownAttack() => mWeaponController.EnableKnockDownAttack(); // 动画调用
        public void EnableKnockUpAttack() => mWeaponController.EnableKnockUpAttack(); // 动画调用

        public void EnableDefenceBreakKnockDownAttack() // 动画调用
        {
            mWeaponController.EnableDefenceBreakKnockDownAttack(); 
            this.SendCommand<WarningOpenCommand>();
        }

        public void EnableDefenceBreakKnockUpAttack() // 动画调用
        {
            mWeaponController.EnableDefenceBreakKnockUpAttack();
            this.SendCommand<WarningOpenCommand>();
        }
        
        public void SkillEnd()
        {
            mCore.AnimationEnd();
            CloseWeapon();
        }
        
        #endregion

        public void Dissolve()
        {
            foreach (var mat in mMaterials)
            {
                mat.SetFloat("_EffectValue", 0f);
            }
            foreach (var mat in mMaterials)
            {
                mat.DOFloat(1, "_EffectValue",  MemoirOfWarAsset.DissolveTime);
            }
        }

        public void Appear()
        {
            foreach (var mat in mMaterials)
            {
                mat.SetFloat("_EffectValue", 1f);
            }
            foreach (var mat in mMaterials)
            {
                mat.DOFloat(0, "_EffectValue", MemoirOfWarAsset.DissolveTime);
            }
        }
        
        public EnemyController ShieldBreak()
        { 
            mCore.ShieldBreak();
            return this;
        }
        
        public EnemyController Stabbed()
        { 
            mCore.Stabbed();
            return this;
        }
        
        public EnemyController Die()
        { 
            mCore.Die();
            return this;
        }
        
        public EnemyController ResetEnemy()
        { 
            mStateMachine.TranslateToState(StartState);
            return this;
        }
        
        public EnemyController AttackNotice()
        { 
            this.SendCommand(new AttackNoticeSpawnCommand(transform));
            return this;
        }
        
        public EnemyController ShieldBreakAttackNotice()
        {
            this.SendCommand(new ShieldBreakAttackNoticeSpawnCommand(transform));
            return this;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            mCore?.OnDrawGizmosSelected();
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}
