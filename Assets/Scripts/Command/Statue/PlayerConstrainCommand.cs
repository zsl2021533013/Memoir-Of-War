using System.Input_System;
using Extension;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Command.Statue
{
    public class PlayerConstrainCommand : AbstractCommand // 限制玩家移动，但是不限制玩家输入
    {
        protected override void OnExecute()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            
            playerModel.StatueCamera.IncreasePriority(); // 这里先写死一些，只给 Statue 使用
            
            playerModel.Controller.ConstrainPlayer();
            
            playerModel.Rigidbody.velocity = Vector3.zero; // 速度必须清零！不然残余速度会使玩家滑行

            InputKit.Instance.DisableMovement();
        }
    }
}