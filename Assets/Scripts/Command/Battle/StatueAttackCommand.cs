using System;
using Architecture;
using Command.Particle_Effect;
using Command.Statue;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Parry_State;
using Controller.Character.Player.Player_State.Player_Sub_State;
using Controller.Statue;
using DG.Tweening;
using Event.Statue;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Command.Battle
{
    public class StatueAttackCommand : AbstractCommand
    {
        private Transform mStatue;
        private StatueAttackType mType;

        private const float CalculateDelay = 0.02f; // 生成特效与数值计算间的微小间隙
        
        public StatueAttackCommand(Transform statue, StatueAttackType type)
        {
            mStatue = statue;
            mType = type;
        }
        
        protected override void OnExecute()
        {
            this.SendCommand(new StatueAttackNoticeSpawnCommand(mStatue, mType));
            // 生成攻击提示
            
            DOTween.Sequence()
                .AppendInterval(MemoirOfWarAsset.StatueAttackNoticeTime)
                .AppendCallback(() =>
                {
                    switch (mType)
                    {
                        case StatueAttackType.Normal:
                            this.SendCommand<StatueNormalAttackSpawnCommand>();
                            break;
                        case StatueAttackType.ShieldBreak:
                            this.SendCommand<StatueShieldBreakAttackCommand>();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    } // 生成攻击特效
                })
                .AppendInterval(CalculateDelay)
                .AppendCallback(() =>
                {
                    var player = this.GetModel<IPlayerModel>().Transform;
                    var controller = this.GetModel<IPlayerModel>().Controller;
                    switch (mType)
                    {
                        case StatueAttackType.Normal when controller.CheckPlayerState<PlayerDefenceState>():
                            this.SendEvent<StatueAttackFailEvent>();
                            this.SendEvent<PlayerDefenceStatueEvent>();
                            break;
                        case StatueAttackType.ShieldBreak when controller.CheckPlayerState<PlayerDodgeState>():
                            this.SendEvent<StatueAttackFailEvent>();
                            this.SendEvent<StatueBulletTimeStartEvent>();
                            break;
                        default:
                            this.SendEvent(new HitSpawnCommand(player.position + 2 * Vector3.up));
                            this.SendEvent<StatueAttackSuccessEvent>();
                            break;
                    } // 数值计算
                });
        }
    }
}