using Architecture;
using Command.Battle;
using QFramework;
using UnityEngine;

namespace Controller.Character.Enemy.Enemy_Base.Controller
{
    /*
     * 该脚本物体下需要挂载三个子物体，名字分别为 Start Point, Mid Point, End Point
     */
    public class EnemyWeaponController : MonoBehaviour, IController
    {
        public bool drawGizmos;
        
        private Transform mRootTransform; // 敌人最上级 Transform
        private Transform mStartPoint;
        private Transform mMidPoint;
        private Transform mEndPoint;

        private bool isWeaponEnable = false;

        private Vector3 mPreStartPointPos;
        private Vector3 mPreMidPointPos;
        private Vector3 mPreEndPointPos;

        private AttackEffectType mAttackEffectType;
        
        private void Awake()
        {
            mStartPoint = transform.Find("Start Point");
            mMidPoint = transform.Find("Mid Point");
            mEndPoint = transform.Find("End Point");
            
            if (!(mStartPoint && mMidPoint && mEndPoint))
            {
                Debug.LogError("Weapon Controller Component Missing");
            }

            mPreStartPointPos = mStartPoint.position;
            mPreEndPointPos = mEndPoint.position;
        }

        private void LateUpdate()
        {
            if (isWeaponEnable && drawGizmos)
            {
                Debug.DrawLine(mPreStartPointPos, mStartPoint.position, Color.red, 1f);
                Debug.DrawLine(mPreEndPointPos, mEndPoint.position, Color.red, 1f);
            }

            var startLerpPos = (mPreStartPointPos + mStartPoint.position) / 2;
            var midLerpPos = (mPreMidPointPos + mMidPoint.position) / 2;
            var endLerpPos = (mPreEndPointPos + mEndPoint.position) / 2;
            
            if (isWeaponEnable)
            {
                DetectCollision(startLerpPos, midLerpPos);
            }
            
            if (isWeaponEnable)
            {
                DetectCollision(midLerpPos, endLerpPos);
            }
            
            if (isWeaponEnable)
            {
                DetectCollision(mStartPoint.position, mMidPoint.position);
            }
            
            if (isWeaponEnable)  // 此处不要简写，因为 DetectCollision 打到人后会关闭 enable
            {
                DetectCollision(mMidPoint.position, mEndPoint.position);
            }

            mPreStartPointPos = mStartPoint.position;
            mPreMidPointPos = mMidPoint.position;
            mPreEndPointPos = mEndPoint.position;
        }

        private void DetectCollision(Vector3 startPos, Vector3 endPos)
        {
            RaycastHit hitInfo;
            Ray ray = new Ray(startPos, endPos - startPos);

            if (drawGizmos)
            {
                Debug.DrawLine(startPos, endPos, Color.blue, 1f);
            }
            
            if (Physics.Raycast(ray, out hitInfo, Vector3.Distance(startPos, endPos), MemoirOfWarAsset.PlayerLayerMask))
            {
                this.SendCommand(new TryToHurtPlayerCommand(mAttackEffectType, mRootTransform, hitInfo.point));
                CloseWeapon();
                return;
            }
        }

        public EnemyWeaponController SetRootTransform(Transform transform)
        {
            mRootTransform = transform;
            return this;
        }
        
        public EnemyWeaponController OpenWeapon()
        { 
            isWeaponEnable = true;
            return this;
        }
        
        public EnemyWeaponController CloseWeapon()
        { 
            isWeaponEnable = false;
            ResetAttackType();
            return this;
        }

        public void EnableKnockDownAttack() => mAttackEffectType = AttackEffectType.KnockDown;
        public void EnableKnockUpAttack() => mAttackEffectType = AttackEffectType.KnockUp;
        public void EnableDefenceBreakKnockDownAttack() => mAttackEffectType = AttackEffectType.DefenceBreakKnockDown;
        public void EnableDefenceBreakKnockUpAttack() => mAttackEffectType = AttackEffectType.DefenceBreakKnockUp;
        private void ResetAttackType() => mAttackEffectType = AttackEffectType.Normal;

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}