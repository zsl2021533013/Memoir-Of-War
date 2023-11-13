using Architecture;
using Command.Battle;
using QFramework;
using UnityEngine;

namespace Controller.Character.Player.Controller
{
    public class PlayerWeaponController : MonoBehaviour, IController
    {
        public bool drawGizmos;
        
        private Transform mStartPoint;
        private Transform mMidPoint;
        private Transform mEndPoint;

        private bool isWeaponEnable = false;

        private Vector3 preStartPointPos;
        private Vector3 preMidPointPos;
        private Vector3 preEndPointPos;

        private void Awake()
        {
            mStartPoint = transform.Find("Start Point");
            mMidPoint = transform.Find("Mid Point");
            mEndPoint = transform.Find("End Point");
            
            if (!(mStartPoint && mMidPoint && mEndPoint))
            {
                Debug.LogError("Weapon Controller Component Missing");
            }

            preStartPointPos = mStartPoint.position;
            preEndPointPos = mEndPoint.position;
        }

        private void LateUpdate()
        {
            if (isWeaponEnable && drawGizmos)
            {
                Debug.DrawLine(preStartPointPos, mStartPoint.position, Color.red, 1f);
                Debug.DrawLine(preEndPointPos, mEndPoint.position, Color.red, 1f);
            }

            var startLerpPos = (preStartPointPos + mStartPoint.position) / 2;
            var midLerpPos = (preMidPointPos + mMidPoint.position) / 2;
            var endLerpPos = (preEndPointPos + mEndPoint.position) / 2;
            
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

            preStartPointPos = mStartPoint.position;
            preMidPointPos = mMidPoint.position;
            preEndPointPos = mEndPoint.position;
        }
        
        private void DetectCollision(Vector3 startPos, Vector3 endPos)
        {
            RaycastHit hitInfo;
            Ray ray = new Ray(startPos, endPos - startPos);

            if (drawGizmos)
            {
                Debug.DrawLine(startPos, endPos, Color.blue, 1f);
            }
            
            if (Physics.Raycast(ray, out hitInfo, Vector3.Distance(startPos, endPos), MemoirOfWarAsset.EnemyLayerMask))
            {
                this.SendCommand(new TryToHurtEnemyCommand(hitInfo.transform, hitInfo.point));
                CloseWeapon();
                return;
            }
        }
        
        public PlayerWeaponController OpenWeapon()
        { 
            isWeaponEnable = true;
            return this;
        }
        
        public PlayerWeaponController CloseWeapon()
        { 
            isWeaponEnable = false;
            return this;
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}
