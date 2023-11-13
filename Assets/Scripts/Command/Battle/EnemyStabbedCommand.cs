using Architecture;
using Event.Character;
using Extension;
using Model.Enemy;
using QFramework;
using UnityEngine;

namespace Command.Battle
{
    public class EnemyStabbedCommand : AbstractCommand
    {
        private Transform mEnemy;

        public EnemyStabbedCommand(Transform enemy)
        {
            mEnemy = enemy;
        }

        protected override void OnExecute()
        {
            var enemyBattleDate = this.GetModel<IEnemyModel>().DataDic.GetValue(mEnemy);

            enemyBattleDate.DecreaseHealth(MemoirOfWarAsset.StabbedDamage);
        }
    }
}