using Architecture;
using Command.Battle;
using Controller.Character.Enemy.Enemy_Base.Controller;
using DG.Tweening;
using Extension;
using Model.Enemy;
using Model.Player;
using QFramework;
using UnityEngine;
using UnityEngine.AI;

namespace Controller.Character.Enemy.Enemy_Base.Core
{
    public enum WalkAroundType
    {
        Idle = 0,
        Left = -1,
        Right = 1,
    }

    public abstract class EnemyCore : IController
    {
        public abstract float ArriveDistance { get; }
        
        protected NavMeshAgent agent;
        protected Animator animator;
        protected Transform playerTrans;
        protected Transform enemyTrans;
        protected StunEffectController stunEffectController;
        protected WalkAroundType type;

        public bool IsAnimationEnd { get; private set; }
        public bool IsShieldBreak { get; private set; }
        public bool IsStabbed { get; private set; }
        public bool IsDead { get; private set; }
        
        public virtual void InitCore(Transform rootTrans)
        {
            this.enemyTrans = rootTrans;
            playerTrans = this.GetModel<IPlayerModel>().Transform;
            animator = rootTrans.GetComponent<Animator>();
            agent = rootTrans.GetComponent<NavMeshAgent>();
            stunEffectController = rootTrans.FindAllChildren("Stun").GetComponent<StunEffectController>();

            SetWalkAroundType(WalkAroundType.Right);
        }
        
        public EnemyCore Play(string animationName)
        {
            if (!animator.isActiveAndEnabled)
            {
                return this;
            } // TODO: 防止敌人被关闭时依旧切换动画，似乎有些问题
            
            var animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
            
            if (animatorInfo.IsName(animationName))
            {
                animator.Play(animationName, 0, 0f); 
            } // 若要重复播放正在播放的动画，则必须调用该方法
            else
            {
                animator.CrossFade(animationName, MemoirOfWarAsset.AnimationFadeTime);
            }
            
            return this;
        }

        public void SetDestination()
        {
            agent.isStopped = false;
            agent.SetDestination(playerTrans.position);
        }
        
        public void SetWalkAroundType(WalkAroundType type)
        {
            this.type = type;
            DOTween.To(() => animator.GetFloat("Walk Around Type"),
                newValue => animator.SetFloat("Walk Around Type", newValue),
                (float)type,
                MemoirOfWarAsset.WalkAroundChangeDurationTime);
        }

        public bool HasArrived()
        {
            var remainingDistance = agent.pathPending ? float.PositiveInfinity : agent.remainingDistance;
            return remainingDistance <= ArriveDistance;
        }

        public void StabbedEnd()
        {
            this.SendCommand(new EnemyStabbedCommand(enemyTrans));
        }

        public float GetPlayerAngle()
        {
            return enemyTrans.GetAngle(playerTrans);
        }
        
        public EnemyCore EnableNavMeshAgentRotation()
        {
            agent.updateRotation = true;
            return this;
        }
        public EnemyCore DisableNavMeshAgentRotation()
        { 
            agent.updateRotation = false;
            return this;
        }

        public EnemyCore EnableStun()
        { 
            stunEffectController.Play();
            return this;
        }

        public EnemyCore DisableStun()
        { 
            stunEffectController.Stop();
            return this;
        }
        
        public bool DetectCanWalkAround()
        {
            var samplePos = type switch
            {
                WalkAroundType.Idle => enemyTrans.position,
                WalkAroundType.Left => enemyTrans.position - enemyTrans.right,
                WalkAroundType.Right => enemyTrans.position + enemyTrans.right,
                _ => enemyTrans.position
            };
            return NavMesh.SamplePosition(samplePos, out _, 0.5f, NavMesh.AllAreas);
        }

        #region External Input

        public EnemyCore AnimationStart()
        { 
            IsAnimationEnd = false;
            return this;
        }
        
        public EnemyCore AnimationEnd()
        { 
            IsAnimationEnd = true;
            return this;
        }
        
        public EnemyCore ShieldBreak()
        { 
            IsShieldBreak = true;
            return this;
        }
        
        public EnemyCore ResetShieldBreak()
        { 
            IsShieldBreak = false;
            return this;
        }
        
        public EnemyCore RecoverShield()
        { 
            this.GetModel<IEnemyModel>().ResetEnemyShield(enemyTrans);
            return this;
        }
        
        public EnemyCore Stabbed()
        { 
            IsStabbed = true;
            return this;
        }
        
        public EnemyCore ResetStabbed()
        { 
            IsStabbed = false;
            return this;
        }
        
        public EnemyCore Die()
        { 
            IsDead = true;
            return this;
        }
        
        public EnemyCore ResetDeath()
        { 
            IsDead = false;
            return this;
        }

        public EnemyCore ResetAllExternalInput()
        {
            IsAnimationEnd = false;
            IsShieldBreak = false;
            IsStabbed = false;
            IsDead = false;

            return this;
        }

        #endregion

        public virtual void OnDrawGizmosSelected()
        {
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}