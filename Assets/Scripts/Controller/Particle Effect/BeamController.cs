using System;
using Architecture;
using Command.Battle;
using DG.Tweening;
using QFramework;
using UnityEngine;

namespace Controller.Particle_Effect
{
    public class BeamController : ParticleEffectController
    {
        private const float DetectRadius = 0.8f;
        
        private const AttackEffectType Type = AttackEffectType.DefenceBreakKnockUp;

        protected override void OnEnable()
        {
            base.OnEnable();

            DOVirtual.DelayedCall(0.1f, DetectAndHitPlayer); 
            /* 直接调用速度过快，不太匹配，故设置延迟，似乎直接调用第一次也会失效，不太清楚原因 */
        }

        private void DetectAndHitPlayer()
        {
            var colliders = Physics.OverlapSphere(transform.position, DetectRadius, MemoirOfWarAsset.PlayerLayerMask);
            if (colliders.Length > 0)
            {
                this.SendCommand(new TryToHurtPlayerCommand(Type, 
                    transform, 
                    colliders[0].ClosestPoint(transform.position)));
            }
        }
    }
}