using DG.Tweening;
using Extension;
using Model.Enemy;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Command.Battle
{
    public class PlayerStabCommand : AbstractCommand
    {
        private Transform mEnemy;

        public PlayerStabCommand(Transform transform)
        {
            mEnemy = transform;
        }
        
        protected override void OnExecute()
        {
            var data = this.GetModel<IEnemyModel>().DataDic.GetValue(mEnemy);
            var controller = data.Controller;
            controller.Stabbed();
                        
            var collider = data.Collider;
            collider.enabled = false;
            DOVirtual.DelayedCall(3f, () => collider.enabled = true);
            
            this.SendCommand(new PlayerEnforceMoveAndRotateCommand(
                mEnemy.position + mEnemy.forward, 
                mEnemy.position));
            /* 先移动，再旋转，避免同时操作产生误差 */
        }
    }
}