using Model.Player;
using QFramework;
using UnityEngine;

namespace Command.Portal
{
    public class PlayerTeleportCommand : AbstractCommand
    {
        private Transform mCurrentPortal;
        private Transform mTargetPortal;

        public PlayerTeleportCommand(Transform currentPortal, Transform targetPortal)
        {
            mCurrentPortal = currentPortal;
            mTargetPortal = targetPortal;
        }
        
        protected override void OnExecute()
        {
            var playerTrans = this.GetModel<IPlayerModel>().Transform;

            #region Position & Rotation

            var m = mTargetPortal.localToWorldMatrix *
                    mCurrentPortal.worldToLocalMatrix *
                    playerTrans.localToWorldMatrix;
            
            playerTrans.position = m.GetColumn(3);
            playerTrans.rotation = m.rotation;

            #endregion

            #region Reset Speed

            var playerRb = this.GetModel<IPlayerModel>().Rigidbody;
            var velocity = playerRb.velocity.magnitude;
            playerRb.velocity = playerTrans.forward * velocity; // 重置速度方向

            #endregion

            #region Reset Camera Position

            this.GetModel<IPlayerModel>().ThirdPersonCameraController.ResetPosition();

            #endregion
        }
    }
}