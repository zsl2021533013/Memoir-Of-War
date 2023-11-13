using Architecture;
using Extension;
using Model;
using Model.Enemy;
using QFramework;
using UnityEngine;
using UnityEngine.AI;

namespace Command.Battle
{
    public class EnemySpawnCommand : AbstractCommand
    {
        private EnemyType mType;
        private Vector3 mPosition;
        private Quaternion mRotation;

        public EnemySpawnCommand(EnemyType type, Vector3 position, Quaternion rotation)
        {
            mType = type;
            mPosition = position;
            mRotation = rotation;
        }
        
        protected override void OnExecute()
        {
            var model = this.GetModel<IEnemyModel>();
            var pool = model.PoolDic.GetValue(mType);
            var enemy = pool.Allocate();
            var data = model.DataDic.GetValue(enemy.transform);
            var controller = data.Controller;
            var agent = data.Agent;
            
            NavMesh.SamplePosition(mPosition, out var hit, 
                MemoirOfWarAsset.NavMeshSampleDistance, NavMesh.AllAreas);

            agent.Warp(hit.position);
            enemy.Rotation(mRotation).SetActive(true);
            controller.ResetEnemy().Appear(); 
            // 对象池回收也会 Reset，此处重复再 Reset 是为了敌人重生时重置 Idle 等待时间 
        }
    }
}