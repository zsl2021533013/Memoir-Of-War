using System.Input_System;
using Architecture;
using Command.Battle;
using Controller.Character.Player.Controller;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Extension;
using Model.Enemy;
using QFramework;
using UnityEngine;

namespace Controller.Character.Player.Core
{
    public class PlayerCore : IController
    {
        public bool IsAnimationEnd { get; private set; }
        public bool IsKnockDown { get; private set; }
        public bool IsKnockUp { get; private set; }
        public bool IsHurt { get; private set; }
        public bool IsDead { get; private set; }
        public bool IsTimelineEnable { get; private set; }

        private PlayerController mController;
        private Animator mAnimator;
        private Rigidbody mRb;
        private CapsuleCollider mMoveCollider;
        private Transform mDefenceColliderTransform;
        private Transform mPlayerTrans;
        private Transform mCamera;
        private bool mIsControlled = false; // 被控制时无法移动
        private PhysicMaterial mZeroFrictionMat;
        private PhysicMaterial mFullFrictionMat;
        
        private struct EnemyPriority // 查找最近的敌人时使用
        {
            public Transform enemy;
            public float dis;
            public bool isShieldBreak;

            public static bool operator >(EnemyPriority a, EnemyPriority b)
            {
                return (a.isShieldBreak == b.isShieldBreak) ? a.dis < b.dis: a.isShieldBreak;
            }
            /*
             如果敌人同样破防或不破防，那么将距离近的优先级高
             如果一个破防一个不破防，那么破防的优先级高
             此时只用判断 a 是不是破防的即可
             因为此时 a 破防则 b 必然不破防，a 不破防则 b 必然破防
             */
            
            public static bool operator <(EnemyPriority a, EnemyPriority b)
            {
                return !(a > b);
            }
        }
        
        public PlayerCore InitCore(Transform rootTrans)
        {
            mPlayerTrans = rootTrans;
            mCamera = Camera.main.transform;
            mMoveCollider = rootTrans.GetComponent<CapsuleCollider>();
            mDefenceColliderTransform = rootTrans.FindAllChildren("Defence Collider");
            mController = rootTrans.GetComponent<PlayerController>();
            mAnimator = rootTrans.GetComponent<Animator>();
            mRb = rootTrans.GetComponent<Rigidbody>();

            var resLoader = ResLoader.Allocate();
            mZeroFrictionMat = resLoader.LoadSync<PhysicMaterial>("Zero Friction");
            mFullFrictionMat = resLoader.LoadSync<PhysicMaterial>("Full Friction");
                
            return this;
        }

        public PlayerCore Play(string animationName)
        {
            var animatorInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
            
            if (animatorInfo.IsName(animationName))
            {
                mAnimator.Play(animationName, 0, 0f); 
            } // 若要重复播放正在播放的动画，则必须调用该方法
            else
            {
                mAnimator.CrossFade(animationName, MemoirOfWarAsset.AnimationFadeTime);
            }
            
            return this;
        }

        public PlayerCore RotatePlayer()
        {
            if (InputKit.Instance.Movement.magnitude >= 0.1f)
            {
                var right = mCamera.right;
                var inputDirection = InputKit.Instance.Movement.normalized;
                var camForward = Vector3.Cross(right, Vector3.up);
                var targetDir = right * inputDirection.x + camForward * inputDirection.y;
                var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                mPlayerTrans.rotation = Quaternion.Lerp(mPlayerTrans.rotation, targetRotation, 0.1f);
            }

            return this;
        }
        
        public PlayerCore RotatePlayerTowardsEnemy(Transform enemy)
        {
            if (!enemy)
            {
                return this;
            }
            
            var targetDir = (enemy.position - mPlayerTrans.position);
            targetDir.y = 0f;
            targetDir.Normalize();
            var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
            mPlayerTrans.rotation = Quaternion.Lerp(mPlayerTrans.rotation, targetRotation, 0.3f);

            return this;
        }

        public PlayerCore MovePlayer()
        {
            var movement = InputKit.Instance.Movement.normalized;
            var targetSpeed = movement.magnitude * MemoirOfWarAsset.MaxSpeed;
            var currentSpeed = mAnimator.GetFloat("Speed");
            var nextSpeed = 0.1f.Lerp(currentSpeed, targetSpeed);
            
            mAnimator.SetFloat("Speed", nextSpeed);
            
            if (mIsControlled) // 如果正处于受击状态，直接返回
            {
                return this;
            }

            #region Slop Detect

            var isRaycastHitFront = Physics.Raycast(
                mPlayerTrans.position + Vector3.up + MemoirOfWarAsset.SlopDetectFrontOffset * mPlayerTrans.forward, 
                Vector3.down, 
                out var frontHit, 
                MemoirOfWarAsset.SlopDetectFrontDistance, 
                MemoirOfWarAsset.GroundLayerMask);
            var frontAngle = isRaycastHitFront ? Vector3.Angle(frontHit.normal, Vector3.up) : float.MaxValue;
            
            var isRaycastHitDown = Physics.Raycast(
                mPlayerTrans.position + Vector3.up, 
                Vector3.down, 
                out var downHit, 
                MemoirOfWarAsset.GroundDetectDistance, 
                MemoirOfWarAsset.GroundLayerMask);
            var downAngle = isRaycastHitDown ? Vector3.Angle(downHit.normal, Vector3.up) : float.MaxValue;

            var slopeNormalPerp = 
                isRaycastHitFront ? Vector3.Cross(mPlayerTrans.right, frontHit.normal) : mPlayerTrans.forward; 
            // 在空中就直接向前，不做额外处理
            
            var velocity = mAnimator.velocity.magnitude * slopeNormalPerp;
            velocity.y = isRaycastHitDown ? velocity.y : mRb.velocity.y; // 如果在空中，就不改变 velocity.y
            
            if (downAngle < MemoirOfWarAsset.FlatGroundMaxAngle && velocity.y < 0f) 
                // 在平面上且下一瞬的速度是向下的
            {
                velocity.y = 0f;
            }
            
            mRb.velocity = velocity;

            #endregion


            #region Physic Material

            mMoveCollider.material = downAngle < MemoirOfWarAsset.FlatGroundMaxAngle || 
                                     frontAngle > MemoirOfWarAsset.SlopMaxAngle || 
                                     InputKit.Instance.Movement != Vector2.zero ? 
                mZeroFrictionMat : mFullFrictionMat;
            // Debug.Log("downAngle is " + downAngle + " frontAngle is " + frontAngle);
            #endregion
            
            /*Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);*/
            
            return this;
        }
        
        public PlayerCore TimelineMoveForward() 
            // 实现类似 timeline 的效果，timeline 不能与 OnAnimatorMove 混用
        {
            var movement = Vector2.up;
            var targetSpeed = movement.magnitude * MemoirOfWarAsset.MaxSpeed;
            var currentSpeed = mAnimator.GetFloat("Timeline Move Speed");
            var nextSpeed = 0.1f.Lerp(currentSpeed, targetSpeed);
            
            mAnimator.SetFloat("Timeline Move Speed", nextSpeed);
            
            if (mIsControlled) // 如果正处于受击状态，直接返回
            {
                return this;
            }

            #region Slop Detect

            var isRaycastHitFront = Physics.Raycast(
                mPlayerTrans.position + Vector3.up + MemoirOfWarAsset.SlopDetectFrontOffset * mPlayerTrans.forward, 
                Vector3.down, 
                out var frontHit, 
                MemoirOfWarAsset.SlopDetectFrontDistance, 
                MemoirOfWarAsset.GroundLayerMask);
            var frontAngle = isRaycastHitFront ? Vector3.Angle(frontHit.normal, Vector3.up) : float.MaxValue;
            
            var isRaycastHitDown = Physics.Raycast(
                mPlayerTrans.position + Vector3.up, 
                Vector3.down, 
                out var downHit, 
                MemoirOfWarAsset.GroundDetectDistance, 
                MemoirOfWarAsset.GroundLayerMask);
            var downAngle = isRaycastHitDown ? Vector3.Angle(downHit.normal, Vector3.up) : float.MaxValue;

            var slopeNormalPerp = 
                isRaycastHitFront ? Vector3.Cross(mPlayerTrans.right, frontHit.normal) : mPlayerTrans.forward; 
            // 在空中就直接向前，不做额外处理
            
            var velocity = mAnimator.velocity.magnitude * slopeNormalPerp;
            velocity.y = isRaycastHitDown ? velocity.y : mRb.velocity.y; // 如果在空中，就不改变 velocity.y
            
            if (downAngle < MemoirOfWarAsset.FlatGroundMaxAngle && velocity.y < 0f) 
                // 在平面上且下一瞬的速度是向下的
            {
                velocity.y = 0f;
            }
            
            mRb.velocity = velocity;

            #endregion


            #region Physic Material

            mMoveCollider.material = downAngle < MemoirOfWarAsset.FlatGroundMaxAngle || 
                                     frontAngle > MemoirOfWarAsset.SlopMaxAngle || 
                                     InputKit.Instance.Movement != Vector2.zero ? 
                mZeroFrictionMat : mFullFrictionMat;
            // Debug.Log("downAngle is " + downAngle + " frontAngle is " + frontAngle);
            #endregion
            
            /*Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);*/
            
            return this;
        }

        public TweenerCore<float, float, FloatOptions> StopTimelineMove()
        {
            return DOTween.To(() => mAnimator.GetFloat("Timeline Move Speed"), 
                value => mAnimator.SetFloat("Timeline Move Speed", value), 
                0f, 
                1f);
        }
        
        public PlayerCore EnableRootMotion()
        {
            mController.EnableRootMotion();
            return this;
        }

        public PlayerCore DisableRootMotion()
        {
            mController.DisableRootMotion();
            return this;
        }
        
        public PlayerCore ResetAnimatorSpeedValue()
        {
            mAnimator.SetFloat("Speed", 0f);
            return this;
        }

        public PlayerCore DisconnectDefenceCollider(out Transform parent)
        {
            parent = mDefenceColliderTransform.parent;
            mDefenceColliderTransform.Parent(null);
            return this;
        }
        
        public PlayerCore ConnectDefenceCollider(Transform parent)
        {
            mDefenceColliderTransform.Parent(parent);
            mDefenceColliderTransform.localPosition = Vector3.zero;
            return this;
        }

        public PlayerCore StartControl()
        {
            mIsControlled = true;
            return this;
        }
        
        public PlayerCore EndControl()
        {
            mIsControlled = false;
            return this;
        }

        public Direction GetDodgeDirection()
        {
            if (InputKit.Instance.Movement.magnitude >= 0.1f)
            {
                var camRight = mCamera.right;
                var inputDirection = InputKit.Instance.Movement.normalized;
                var camForward = Vector3.Cross(camRight, Vector3.up);
                var targetDir = camRight * inputDirection.x + camForward * inputDirection.y;
                var playerRight = mPlayerTrans.right;
                
                var angle = Mathf.Acos(Vector2.Dot(playerRight, new Vector2(targetDir.x, targetDir.z))) 
                            * Mathf.Rad2Deg;
                
                return angle > 90 ? Direction.Left : Direction.Right;
            }
            
            return Direction.Right;
        }
        
        public Transform GetTheClosestEnemy()
        {
            var ans = new EnemyPriority()
            {
                enemy = null,
                dis = float.MaxValue,
                isShieldBreak = false
            };
            var enemyColliders = Physics.OverlapSphere(
                mPlayerTrans.position, 
                MemoirOfWarAsset.RotateRangeWhileAttacking, 
                MemoirOfWarAsset.EnemyLayerMask);

            var dataDic = this.GetModel<IEnemyModel>().DataDic;
            foreach (var enemyCollider in enemyColliders)
            {
                var enemyTrans = enemyCollider.transform;
                var currentEnemy = new EnemyPriority()
                {
                    enemy = enemyTrans,
                    dis = Vector3.Distance(enemyTrans.position, mPlayerTrans.position),
                    isShieldBreak = dataDic.GetValue(enemyCollider.transform).Controller.IsShieldBreak
                };
                
                if (currentEnemy > ans)
                {
                    ans = currentEnemy;
                }
            }

            return ans.enemy;
        }

        public bool CheckStabbed()
        {
            var enemy = GetTheClosestEnemy();
            if (!enemy)
            {
                return false;
            }

            var data = this.GetModel<IEnemyModel>().DataDic.GetValue(enemy);
            var dir = (mPlayerTrans.position - enemy.position).normalized;
            return data.Controller.IsShieldBreak && 
                   Vector3.Distance(enemy.position, mPlayerTrans.position) < MemoirOfWarAsset.StabDistance && 
                   Vector3.Dot(dir, enemy.forward) > MemoirOfWarAsset.StabAngle;
        }

        public PlayerCore PlayerStab()
        {
            this.SendCommand(new PlayerStabCommand(GetTheClosestEnemy()));
            return this;
        }

        public PlayerCore StartImmortal()
        {
            this.SendCommand<PlayerStartImmortalCommand>();
            return this;
        }

        public PlayerCore EndImmortal()
        {
            this.SendCommand<PlayerEndImmortalCommand>();
            return this;
        }

        #region Outside Input

        public PlayerCore AnimationStart()
        { 
            IsAnimationEnd = false;
            return this;
        }
        
        public PlayerCore AnimationEnd()
        { 
            IsAnimationEnd = true;
            return this;
        }
        
        public PlayerCore KnockDown()
        { 
            IsKnockDown = true;
            return this;
        }
        
        public PlayerCore KnockUp()
        { 
            IsKnockUp = true;
            return this;
        }
        
        public PlayerCore Hurt()
        { 
            IsHurt = true;
            return this;
        }
        
        public PlayerCore UseHurt()
        { 
            IsHurt = false;
            return this;
        }
        
        public PlayerCore Die()
        { 
            IsDead = true;
            return this;
        }
        
        public PlayerCore UseDeath()
        { 
            IsDead = false;
            return this;
        }
        
        public PlayerCore EnableTimeline()
        { 
            IsTimelineEnable = true;
            return this;
        }
        
        public PlayerCore DisableTimeline()
        { 
            IsTimelineEnable = false;
            return this;
        }

        public PlayerCore ResetAllExternalInput()
        {
            IsKnockDown = false;
            IsKnockUp = false;
            IsHurt = false;
            IsDead = false;
            return this;
        }

        public PlayerCore UseControlled()
        {
            IsKnockUp = false;
            IsKnockDown = false;
            return this;
        }

        #endregion

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(mPlayerTrans.position, MemoirOfWarAsset.RotateRangeWhileAttacking);
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}
