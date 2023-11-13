using Architecture;
using DG.Tweening;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Command.Battle
{
    public class PlayerEnforceMoveAndRotateCommand : AbstractCommand
    {
        private Vector3 mPos;
        private Vector3 mLookAtPs;

        public PlayerEnforceMoveAndRotateCommand(Vector3 pos, Vector3 lookAtPos)
        {
            mPos = pos;
            mLookAtPs = lookAtPos;
        }
        
        protected override void OnExecute()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            var player = playerModel.Transform;
            playerModel.Rigidbody.velocity = Vector3.zero;
            
            DOTween.Sequence()
                .Append(player.DOMove(mPos, MemoirOfWarAsset.PlayerEnforceMoveOrRotateDuration))
                .Append(player.DOLookAt(mLookAtPs, MemoirOfWarAsset.PlayerEnforceMoveOrRotateDuration)); 
        }
    }
}