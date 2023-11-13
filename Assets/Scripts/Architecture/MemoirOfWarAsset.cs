using UnityEngine;

namespace Architecture
{
    public enum EnemyType
    {
        Warrior,
        Shaman,
        Chief,
        BigOrk,
        RedDemon,
        DarkElf,
        UndeadKnight,
        Janissary
    }
    
    public enum ParticleType
    {
        AttackNotice,
        ShieldBreakAttackNotice,
        Beam,
        Defence,
        EnemyFireball,
        PlayerFireball,
        Hit,
        RockShoot,
        StatueNormalAttack,
        StatueShieldBreakAttack
    }

    public enum AttackEffectType
    {
        Normal,
        KnockUp,
        KnockDown,
        DefenceBreakKnockUp,
        DefenceBreakKnockDown,
        Fireball
    }

    public enum DialogueCharacterType
    {
        Xin,
        WuShang
    }

    public enum BGMType
    {
        MainTheme,
        SubTheme,
        BossRoom1,
        BossRoom2,
        BossRoom3
    }

    public enum SoundEffectType
    {
        EntranceMove,
    }

    public class MemoirOfWarAsset
    {
        #region UI

        public const float PerCharDuration = 0.05f;
        public const float PerDialogueDelay = 2.5f;
        
        public const float UIFadeDuration = 0.6f;
        public const float SceneNameDuration = 2f;
        public const float SceneNameShowTime = 3f;
        public const float SceneNameDelay = 2f;
        public const float SceneNameMainMenuDelay = 0.6f;
        public const float EntranceDuration = 1.5f;
        public static readonly Vector3 EntranceLevel0Rotation = new(0, 180, 0);
        public static readonly Vector3 EntranceLevel1Rotation = new(0, -90, 0);
        public static readonly Vector3 EntranceLevel2Rotation = new(0, 0, 0);
        public static readonly Vector3 EntranceLevel3Rotation = new(0, 90, 0);
        
        #endregion
        
        #region Ground

        public static readonly int GroundLayerMask = LayerMask.GetMask("Ground");
        public static readonly int GroundLayer = LayerMask.NameToLayer("Ground");
        public const float Gravity = -9.81f;
        public const float GravityScale = 5f;
        public const float GroundDetectDistance = 1.2f;
        public const float SlopDetectFrontOffset = 0.2f;
        public const float SlopDetectFrontDistance = 2f;
        public const float FlatGroundMaxAngle = 10f;
        public const float SlopMaxAngle = 50f;

        #endregion
        
        #region Character

        public const float AnimationFadeTime = 0.1f;
        public const float AnimationBulletTimeSpeed = 0.1f;

        #endregion
        
        #region Player

        public static readonly int PlayerLayerMask = LayerMask.GetMask("Player");
        public static readonly int PlayerLayer = LayerMask.NameToLayer("Player");
        public const float MaxSpeed = 4.490565f; // 该值由动画根运动得出
        public const float RotateRangeWhileAttacking = 8f;
        public const float BulletTime = 3f;
        public const float DefaultKnockForce = 3f;
        public const float KnockDownForce = 5f;
        public const float KnockUpForce = 7f;
        public const float StabDistance = 2.5f;
        public const float StabAngle = 0.5f;
        public const float WarningTime = 0.15f;
        public const float PlayerEnforceMoveOrRotateDuration = 0.2f;
        public const float PlayerKnockBackTime = 0.5f;
        public const float PlayerDefenceInterval = 0.6f;
        
        public const float StatueAttackInterval = 0.8f;
        public const float StatueAttackNoticeTime = 0.4f;
        public const float StatueTriggerOffset = 6f;
        public const float StatuePlayerOffset = 8f;
        public const float StatueDetectRadius = 0.5f;
        public const float StatueBulletTime = 0.2f;
        
        public const float HitBulletTime = 0.06f;
        
        #endregion

        #region Enemy

        public static readonly int EnemyLayerMask = LayerMask.GetMask("Enemy");
        public static readonly int EnemyLayer = LayerMask.NameToLayer("Enemy");
        public const float AngleSpeed = 1000f;
        public const float IdleTime = 3f;
        public const float WalkAroundTime = 1.5f;
        public const float BodyTime = 4f;
        public const float DissolveTime = 3f;
        public const float StabbedDamage = 40f;
        public const float WalkAroundChangeDurationTime = 0.2f;
        public const float NavMeshSampleDistance = 10f;

        #endregion

        #region Battlefield

        public static readonly int AirWallLayerMask = LayerMask.GetMask("Air Wall");
        public static readonly int AirWallLayer = LayerMask.NameToLayer("Air Wall");

        #endregion

        #region Audio

        public const float BGMTranslationDuration = 2f;
        public const float BGMStopDuration = 5f;
        public const float BGMSubThemeVolume = 0.18f;

        #endregion
    }
}