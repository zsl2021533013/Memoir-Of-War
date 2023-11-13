using System.Collections.Generic;
using Architecture;
using Command.Particle_Effect;
using Controller.Character.Enemy.Enemy_Base.Core;
using DG.Tweening;
using Model.Player;
using QFramework;
using UnityEngine;
using UnityEngine.AI;

namespace Controller.Character.Enemy.Dark_Elf.Core
{
    public class DarkElfCore : EnemyCore
    {
        private const float TeleportFarDis = 8f;
        private const float TeleportCloseDis = 1.5f;
        private const float QuickDissolveTime = 1f;
        
        private List<Material> mMaterials = new List<Material>();
        
        public override float ArriveDistance  => 2f;

        public override void InitCore(Transform rootTrans)
        {
            base.InitCore(rootTrans);
            
            var renders = rootTrans.GetComponentsInChildren<Renderer>();
            foreach (var render in renders)
            {
                mMaterials.AddRange(render.materials);
            }
        }
        
        public void QuickDissolve()
        {
            foreach (var mat in mMaterials)
            {
                mat.SetFloat("_EffectValue", 0f);
            }
            foreach (var mat in mMaterials)
            {
                mat.DOFloat(1, "_EffectValue",  QuickDissolveTime);
            }
        }

        public void QuickAppear()
        {
            foreach (var mat in mMaterials)
            {
                mat.SetFloat("_EffectValue", 1f);
            }
            foreach (var mat in mMaterials)
            {
                mat.DOFloat(0, "_EffectValue", QuickDissolveTime);
            }
        }

        public Vector3 GetCloseTeleportPosition()
        {
            var playerTrans = this.GetModel<IPlayerModel>().Transform;
            var playerPos = playerTrans.position;

            var targetPos = new[] {
                playerPos + TeleportCloseDis * playerTrans.forward,
                playerPos + TeleportCloseDis * -playerTrans.right,
                playerPos + TeleportCloseDis * playerTrans.right,
                playerPos + TeleportCloseDis * -playerTrans.forward
            };

            foreach (var pos in targetPos)
            {
                if (NavMesh.SamplePosition(pos, out var hit,
                        MemoirOfWarAsset.NavMeshSampleDistance, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            
            return enemyTrans.position;
        }
        
        public Vector3 GetFarTeleportPosition()
        {
            var playerTrans = this.GetModel<IPlayerModel>().Transform;
            var playerPos = playerTrans.position;

            var targetPos = new[] {
                playerPos + TeleportFarDis * playerTrans.forward,
                playerPos + TeleportFarDis * -playerTrans.right,
                playerPos + TeleportFarDis * playerTrans.right,
                playerPos + TeleportFarDis * -playerTrans.forward
            };

            foreach (var pos in targetPos)
            {
                if (NavMesh.SamplePosition(pos, out var hit,
                        MemoirOfWarAsset.NavMeshSampleDistance, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            
            return enemyTrans.position;
        }

        public DarkElfCore Teleport(Vector3 pos)
        {
            DOTween.Sequence()
                .AppendCallback(QuickDissolve)
                .AppendInterval(QuickDissolveTime)
                .AppendCallback(() => agent.Warp(pos))
                .AppendCallback(() => enemyTrans.LookAt(playerTrans))
                .AppendCallback(QuickAppear);
            return this;
        }
        
        public void ShootFireball() =>
            this.SendCommand(new EnemyFireballSpawnCommand(enemyTrans,
                enemyTrans.position + 0.5f * enemyTrans.forward + 0.5f * enemyTrans.up));
    }
}