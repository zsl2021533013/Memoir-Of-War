using Architecture;
using Command.Particle_Effect;
using Controller.Character.Player.Player_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Doge_State;
using Controller.Character.Player.Player_State.Player_Skill_State.Player_Parry_State;
using Controller.Character.Player.Player_State.Player_Sub_State;
using Event;
using Event.Character;
using Event.Character.Enemy;
using Event.Character.Player;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Command.Battle
{
    public class PlayerHurtCommand : AbstractCommand
    {
        private AttackEffectType mType;
        private Transform mEnemy;
        private Vector3 mHitPos;

        public PlayerHurtCommand(AttackEffectType type, Transform enemy, Vector3 hitPos)
        {
            mType = type;
            mEnemy = enemy;
            mHitPos = hitPos;
        }

        protected override void OnExecute()
        {
            var model = this.GetModel<IPlayerModel>();
            var controller = model.Controller;

            if (model.IsBulletTime)
            {
                return;
            }

            if (controller.CheckPlayerState<PlayerControlledState>() ||
                controller.CheckPlayerState<PlayerHurtState>())
            {
                return;
            }

            switch (mType)
            {
                case AttackEffectType.DefenceBreakKnockDown:
                case AttackEffectType.DefenceBreakKnockUp:
                    if (controller.CheckPlayerState<PlayerDodgeLeftState>()
                        || controller.CheckPlayerState<PlayerDodgeRightState>())
                    {
                        this.SendEvent<BulletTimeStartEvent>();
                    }
                    else
                    {
                        this.SendEvent(new HitEffectSpawnEvent(mHitPos));
                        this.SendEvent(new PlayerHurtEvent(mEnemy, mType));
                    }
                    break;
                case AttackEffectType.Fireball:
                    if (controller.CheckPlayerState<PlayerDefenceState>())
                    {
                        this.SendEvent(new PlayerDefenceSuccessEvent(mType, mHitPos));
                        this.SendEvent(new FireballParriedEvent(mEnemy));
                        this.SendCommand(new PlayerFireballSpawnCommand(mEnemy, mHitPos));
                    }
                    else if(controller.CheckPlayerState<PlayerDodgeState>()) { }
                    else
                    {
                        this.SendEvent(new HitEffectSpawnEvent(mHitPos));
                        this.SendEvent(new PlayerHurtEvent(mEnemy, mType));
                    }
                    break;
                case AttackEffectType.Normal:
                case AttackEffectType.KnockUp:
                case AttackEffectType.KnockDown:
                default:
                    // 弹刀
                    if (controller.CheckPlayerState<PlayerDefenceState>())
                    {
                        this.SendEvent(new PlayerDefenceSuccessEvent(mType, mHitPos));
                        this.SendEvent(new EnemyParriedEvent(mEnemy));
                    }
            
                    // 闪避
                    else if (controller.CheckPlayerState<PlayerDodgeState>()) { }

                    // 受伤
                    else
                    {
                        this.SendEvent(new HitEffectSpawnEvent(mHitPos));
                        this.SendEvent(new PlayerHurtEvent(mEnemy, mType));
                    }
                    break;
            }
        }
    }
}