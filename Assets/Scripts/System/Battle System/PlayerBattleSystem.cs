using System.Input_System;
using System.Linq;
using Architecture;
using DG.Tweening;
using Event.Character;
using Event.Character.Player;
using Event.Statue;
using Extension;
using Model.Enemy;
using Model.Particle_Effect;
using Model.Player;
using QFramework;
using UnityEngine;

namespace System.Battle_System
{
    public interface IPlayerBattleSystem : ISystem
    {
    }
    
    public class PlayerBattleSystem : AbstractSystem, IPlayerBattleSystem
    {
        protected override void OnInit()
        {
            this.RegisterEvent<BulletTimeStartEvent>(e =>
            {
                Debug.Log("Bullet Time start");

                #region Animator

                this.GetModel<IPlayerModel>().IsBulletTime = true;
                
                var enemyBattleDataDic = this.GetModel<IEnemyModel>().DataDic;
                foreach (var data in enemyBattleDataDic.Select(pair => pair.Value))
                {
                    data.Animator.SetSpeed(MemoirOfWarAsset.AnimationBulletTimeSpeed);
                    data.Agent.StopRotation();
                }
                
                #endregion

                #region Particle

                var particleControllers = this.GetModel<IParticleEffectModel>()
                    .BulletTimeStart()
                    .Controllers;
                foreach (var controller in particleControllers)
                {
                    controller.SetPlaySpeed(MemoirOfWarAsset.AnimationBulletTimeSpeed);
                }

                #endregion

                DOVirtual.DelayedCall(MemoirOfWarAsset.BulletTime, this.SendEvent<BulletTimeEndEvent>);
            });
            
            this.RegisterEvent<BulletTimeEndEvent>(e =>
            {
                Debug.Log("Bullet Time end");
                
                var playerModel = this.GetModel<IPlayerModel>();
                playerModel.IsBulletTime = false;
                playerModel.Animator.ResetSpeed();
                
                var dataDic = this.GetModel<IEnemyModel>().DataDic;
                foreach (var data in dataDic.Select(pair => pair.Value))
                {
                    data.Animator.ResetSpeed();
                    data.Agent.StartRotation();
                }
                
                var particleControllers = this.GetModel<IParticleEffectModel>()
                    .BulletTimeEnd()
                    .Controllers;
                foreach (var controller in particleControllers)
                {
                    controller.ResetPlaySpeed();
                }
            });
            
            this.RegisterEvent<HitBulletTimeStartEvent>(e =>
            {
                Debug.Log("Hit Bullet Time start");
                
                var enemyBattleDataDic = this.GetModel<IEnemyModel>().DataDic;
                foreach (var data in enemyBattleDataDic.Select(pair => pair.Value))
                {
                    data.Animator.SetSpeed(MemoirOfWarAsset.AnimationBulletTimeSpeed);
                    data.Agent.StopRotation();
                }

                var playerModel = this.GetModel<IPlayerModel>();
                playerModel.Animator.SetSpeed(MemoirOfWarAsset.AnimationBulletTimeSpeed);

                DOVirtual.DelayedCall(MemoirOfWarAsset.HitBulletTime, this.SendEvent<HitBulletTimeEndEvent>);
            });
            
            this.RegisterEvent<HitBulletTimeEndEvent>(e =>
            {
                Debug.Log("Hit Bullet Time end");
                
                var playerModel = this.GetModel<IPlayerModel>();
                playerModel.Animator.ResetSpeed();
                
                if (playerModel.IsBulletTime) // 若此时处于子弹时间，就只回复玩家的动画速度，否则会一同恢复敌人的
                {
                    return;
                }
                
                var dataDic = this.GetModel<IEnemyModel>().DataDic;
                foreach (var data in dataDic.Select(pair => pair.Value))
                {
                    data.Animator.ResetSpeed();
                    data.Agent.StartRotation();
                }
            });

            this.RegisterEvent<PlayerDefenceSuccessEvent>(e =>
            {
                InputKit.Instance.EnableDefence();
                
                var controller = this.GetModel<IPlayerModel>().Controller;
                switch (e.type)
                {
                    case AttackEffectType.KnockDown:
                        controller.AddForce(MemoirOfWarAsset.KnockDownForce);
                        controller.Impulse();
                        break;
                    case AttackEffectType.KnockUp or AttackEffectType.Fireball:
                        controller.AddForce(MemoirOfWarAsset.KnockUpForce);
                        controller.Impulse();
                        break;
                    default:
                        controller.AddForce(MemoirOfWarAsset.DefaultKnockForce);
                        break;
                }
            });

            this.RegisterEvent<PlayerHurtEvent>(e =>
            {
                var model = this.GetModel<IPlayerModel>();
                var controller = model.Controller;
                var transform = model.Transform;

                var targetPos = e.enemyTransform.position;
                targetPos.y = this.GetModel<IPlayerModel>().Transform.position.y;

                switch (e.type)
                {
                    case AttackEffectType.Normal:
                        transform.LookAt(targetPos);
                        controller.GetHit();
                        controller.AddForce(MemoirOfWarAsset.DefaultKnockForce);
                        break;
                    case AttackEffectType.KnockDown or AttackEffectType.DefenceBreakKnockDown:
                        transform.LookAt(targetPos);
                        controller.KnockDown();
                        controller.Impulse();
                        break;
                    case AttackEffectType.KnockUp or AttackEffectType.DefenceBreakKnockUp or AttackEffectType.Fireball:
                        transform.LookAt(targetPos);
                        controller.KnockUp();
                        controller.Impulse();
                        break;
                }
                
                this.GetModel<IPlayerModel>().DecreaseHealth(5f);
            });
            
            this.RegisterEvent<StatueAttackSuccessEvent>(e =>
            {
                var controller = this.GetModel<IPlayerModel>().Controller;

                controller.KnockUp();
                
                this.GetModel<IPlayerModel>().DecreaseHealth(5f);
            });

            this.RegisterEvent<PlayerDefenceStatueEvent>(e =>
            {
                var controller = this.GetModel<IPlayerModel>().Controller;
                controller.Impulse();
            });
            
            this.RegisterEvent<StatueBulletTimeStartEvent>(e =>
            {
                var playerModel = this.GetModel<IPlayerModel>();
                playerModel.Animator.SetSpeed(MemoirOfWarAsset.AnimationBulletTimeSpeed);

                var enemyBattleDataDic = this.GetModel<IEnemyModel>().DataDic;
                foreach (var data in enemyBattleDataDic.Select(pair => pair.Value))
                {
                    data.Animator.SetSpeed(MemoirOfWarAsset.AnimationBulletTimeSpeed);
                    data.Agent.StopRotation();
                }

                var particleControllers = this.GetModel<IParticleEffectModel>()
                    .BulletTimeStart()
                    .Controllers;
                foreach (var controller in particleControllers)
                {
                    controller.SetPlaySpeed(MemoirOfWarAsset.AnimationBulletTimeSpeed);
                }

                DOVirtual.DelayedCall(MemoirOfWarAsset.StatueBulletTime, 
                    this.SendEvent<BulletTimeEndEvent>);
            });
        }
    }
}