using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Architecture;
using Event.Character.Enemy;
using Extension;
using QFramework;
using UnityEngine;

namespace Model.Enemy
{
    public interface IEnemyModel : IModel
    {
        public Dictionary<EnemyType, SimpleObjectPool<GameObject>> PoolDic { get; }
        public Dictionary<Transform, EnemyData> DataDic { get; }

        public void ResetEnemyShield(Transform enemy);
    }
    
    public class EnemyModel : AbstractModel, IEnemyModel
    {
        private ResLoader mResLoader;
        
        private Dictionary<EnemyType, EnemyAttribute> mTemplateAttributeDic
            = new Dictionary<EnemyType, EnemyAttribute>();

        public Dictionary<EnemyType, SimpleObjectPool<GameObject>> PoolDic  { get; }
            = new Dictionary<EnemyType, SimpleObjectPool<GameObject>>();
        
        public Dictionary<Transform, EnemyData> DataDic { get; }
            = new Dictionary<Transform, EnemyData>();

        protected override void OnInit()
        {
            mResLoader = ResLoader.Allocate();

            InitAttributeDic();
            
            InitPool();
        }

        #region Attribute

        private void InitAttributeDic() // 初始化属性数据库，直接写死于代码
        {
            // AddTemplateAttribute(EnemyType.Skeleton, new EnemyAttribute(15f, 10f));
            AddTemplateAttribute(EnemyType.Warrior, new EnemyAttribute(15f, 10f));
            AddTemplateAttribute(EnemyType.Shaman, new EnemyAttribute(15f, 10f));
            AddTemplateAttribute(EnemyType.Chief, new EnemyAttribute(40f, 20f));
            AddTemplateAttribute(EnemyType.BigOrk, new EnemyAttribute(50f, 20f));
            AddTemplateAttribute(EnemyType.RedDemon, new EnemyAttribute(120f, 30f));
            AddTemplateAttribute(EnemyType.UndeadKnight, new EnemyAttribute(160f, 40f));
            AddTemplateAttribute(EnemyType.DarkElf, new EnemyAttribute(200f, 60f));
            AddTemplateAttribute(EnemyType.Janissary, new EnemyAttribute(240f, 40f));
        }

        private void AddTemplateAttribute(EnemyType type, EnemyAttribute attribute)
        {
            if (mTemplateAttributeDic.ContainsKey(type))
            { 
                mTemplateAttributeDic[type] = attribute;
            }
            else
            {
                mTemplateAttributeDic.Add(type, attribute);
            }
        }

        #endregion

        #region Object Pool
        
        private void InitPool()
        {
            foreach (EnemyType type in Enum.GetValues(typeof(EnemyType))) // 初始化所有类型
            {
                PoolDic.Add(type, new SimpleObjectPool<GameObject>(
                    () =>
                    {
                        var prefab = mResLoader.LoadSync<GameObject>(type.ToString().InsertSpace());
                        var gameObject = prefab.Instantiate().Name(type.ToString());
                        var templateAttribute = mTemplateAttributeDic.GetValue(type);
                        
                        gameObject.SetActive(false);
                        AddEnemyData(type, gameObject.transform, new EnemyAttribute(templateAttribute));

                        var controller = DataDic[gameObject.transform].Controller;
                        controller.Init();
                        
                        return gameObject;
                    },
                    (gameObject) =>
                    {
                        var data = DataDic.GetValue(gameObject.transform);
                        
                        ResetEnemyData(data);
                        data.Controller.ResetEnemy();
                        data.Transform.Layer("Enemy");
                        data.Collider.enabled = true;
                        gameObject.SetActive(false); 
                    })); // 不要在注册中初始化加载，生命不同，容易与 unity 冲突
            }
        }

        #endregion

        #region Enemy Data

        private void InitEnemyData(EnemyData data)
        {
            data.Attribute.Health.Register(newValue =>
            {
                if (newValue <= 0f)
                {
                    this.SendEvent(new EnemyDieEvent(data.Transform));
                }
            });

            data.Attribute.Shield.Register(newValue =>
            {
                if (newValue <= 0f)
                {
                    this.SendEvent(new EnemyShieldBreakEvent(data.Transform));
                }
            });
        }
        
        private void AddEnemyData(EnemyType type, Transform enemy, EnemyAttribute attribute)
        {
            var data = new EnemyData(type, enemy, attribute);
            InitEnemyData(data); // 为其添加事件
            
            DataDic[enemy] = data;
        }
        
        private void ResetEnemyData(EnemyData data)
        {
            var type = data.Type;
            var templateAttribute = mTemplateAttributeDic.GetValue(type);
            data.ResetAttribute(templateAttribute);
        }

        public void ResetEnemyShield(Transform enemy)
        {
            var data = DataDic.GetValue(enemy);
            var type = data.Type;
            var templateAttribute = mTemplateAttributeDic.GetValue(type);
            data.ResetShield(templateAttribute);
        }

        #endregion
    }
}