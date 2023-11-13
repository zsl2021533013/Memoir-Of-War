using Controller.Character.Enemy.Enemy_Base.Core;
using DG.Tweening;
using Extension;
using UnityEngine;

namespace Controller.Character.Enemy.Undead_Knight.Core
{
    public class UndeadKnightCore : EnemyCore
    {
        private const float Attack4Distance = 4f;
        
        public override float ArriveDistance  => 4f;

        public bool HasArrivedAttack4Range()
        {
            var remainingDistance = agent.pathPending ? float.PositiveInfinity : agent.remainingDistance;
            return remainingDistance <= ArriveDistance + Attack4Distance;
        }
    }
}