using System;
using System.Battle_System;
using System.Music_System;
using System.Particle_System;
using Model.Enemy;
using Model.Particle_Effect;
using Model.Player;
using QFramework;

namespace Architecture
{
    public class MemoirOfWar : Architecture<MemoirOfWar>
    {
        protected override void Init()
        {
            RegisterSystem<IEnemyBattleSystem>(new EnemyBattleSystem());
            RegisterSystem<IPlayerBattleSystem>(new PlayerBattleSystem());
            RegisterSystem<IParticleEffectSystem>(new ParticleEffectSystem());
            RegisterSystem<IMusicSystem>(new MusicSystem());
        
            RegisterModel<IEnemyModel>(new EnemyModel());
            RegisterModel<IPlayerModel>(new PlayerModel());
            RegisterModel<IParticleEffectModel>(new ParticleEffectModel());
        }
    }
}
