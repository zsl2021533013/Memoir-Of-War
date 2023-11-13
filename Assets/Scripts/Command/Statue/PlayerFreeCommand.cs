using System.Input_System;
using Extension;
using Model.Player;
using QFramework;

namespace Command.Statue
{
    public class PlayerFreeCommand : AbstractCommand // 释放玩家，允许移动
    {
        protected override void OnExecute()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            
            playerModel.ThirdPersonCameraController.ResetPosition();
            
            playerModel.StatueCamera.ResetPriority();
            
            playerModel.Controller.FreePlayer();
            
            InputKit.Instance.EnableMovement();
        }
    }
}