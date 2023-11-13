using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Architecture;
using Controller.Particle_Effect;
using Extension;
using QFramework;
using UnityEngine;

namespace Model.Particle_Effect
{
    public interface IParticleEffectModel : IModel
    {
        public Dictionary<ParticleType, SimpleObjectPool<GameObject>> PoolDic { get; }
        public List<ParticleEffectController> Controllers { get; }
        
        public IParticleEffectModel BulletTimeStart();
        public IParticleEffectModel BulletTimeEnd();
    }
    
    public class ParticleEffectModel : AbstractModel, IParticleEffectModel
    {
        private ResLoader mResLoader = ResLoader.Allocate();

        private bool mIsBulletTime = false;
        
        public Dictionary<ParticleType, SimpleObjectPool<GameObject>> PoolDic { get; } = new();
        // TODO: 对象池在切换场景时是否应当自动删去已消失的对象？
        public List<ParticleEffectController> Controllers { get; private set; } = new();

        protected override void OnInit()
        {
            InitPool();
        }

        private void InitPool()
        {
            foreach (ParticleType type in Enum.GetValues(typeof(ParticleType)))
            {
                PoolDic.Add(type, new SimpleObjectPool<GameObject>(
                    () =>
                    {
                        var prefab = mResLoader.LoadSync<GameObject>(type.ToString().InsertSpace());
                        var gameObject = prefab.Instantiate().Name(type.ToString());
                        gameObject.SetActive(true);

                        var controller = gameObject.GetComponentInChildren<ParticleEffectController>();
                        Controllers.Add(controller);
                        if (mIsBulletTime) // 如果当前处于子弹时间，则生产的特效速度也要变化
                        {
                            controller.SetPlaySpeed(MemoirOfWarAsset.AnimationBulletTimeSpeed);
                        }
                        
                        return gameObject;
                    },
                    (gameObject) =>
                    {
                        gameObject.SetActive(false); 
                    })); // 不要在注册中初始化加载，生命不同，容易与 unity 冲突
            }
        }

        public IParticleEffectModel BulletTimeStart()
        { 
            mIsBulletTime = true;
            return this;
        }
        
        public IParticleEffectModel BulletTimeEnd()
        { 
            mIsBulletTime = false;
            return this;
        }
    }
}