using Architecture;
using DG.Tweening;
using Event.Character.Enemy;
using Extension;
using Model.Enemy;
using QFramework;
using UnityEngine;

namespace System.Battle_System
{
    public interface IEnemyBattleSystem : ISystem
    {
    }
    
    public class EnemyBattleSystem : AbstractSystem, IEnemyBattleSystem
    {
        protected override void OnInit()
        {
            this.RegisterEvent<EnemyHurtEvent>(e =>
            {
                var date = this.GetModel<IEnemyModel>().DataDic.GetValue(e.enemyTransform);
                if (e.type == AttackEffectType.Fireball)
                {
                    date.DecreaseHealth(15f);
                    date.DecreaseShield(20f);
                }
                else
                {
                    date.DecreaseHealth(5f);
                }
            });
            
            this.RegisterEvent<EnemyDieEvent>(e =>
            {
                Debug.Log( e.Enemy.name + " die");

                var model = this.GetModel<IEnemyModel>();
                var enemy = e.Enemy;
                var data = model.DataDic.GetValue(enemy);
                var collider = data.Collider;
                var controller = data.Controller;
                
                enemy.Layer("Default");
                controller.Die();
                controller.CloseWeapon();

                DOTween.Sequence()
                    .AppendInterval(MemoirOfWarAsset.BodyTime)
                    .AppendCallback(() => collider.enabled = false)
                    .AppendCallback(() => controller.Dissolve())
                    .AppendInterval(MemoirOfWarAsset.DissolveTime)
                    .AppendCallback(() =>
                    {
                        var pool = model.PoolDic.GetValue(data.Type);
                        pool.Recycle(enemy.gameObject);
                    });
            });

            this.RegisterEvent<EnemyParriedEvent>(e =>
            {
                Debug.Log( e.Enemy.name + " is parried");
                
                var enemy = e.Enemy;

                if (!this.GetModel<IEnemyModel>().DataDic.ContainsKey(enemy))
                {
                    return; // 数据中不包含此物体，为粒子特效
                }
                
                var data = this.GetModel<IEnemyModel>().DataDic.GetValue(enemy);
                
                data.DecreaseShield(10f);
            });

            this.RegisterEvent<EnemyShieldBreakEvent>(e =>
            {
                Debug.Log(e.Enemy.name + " shield break");
                
                var enemy = e.Enemy;
                var data = this.GetModel<IEnemyModel>().DataDic.GetValue(enemy);
                var controller = data.Controller;
                
                controller.ShieldBreak();
            });
        }
    }
}