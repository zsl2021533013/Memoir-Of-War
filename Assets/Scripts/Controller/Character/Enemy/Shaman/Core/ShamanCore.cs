using Command.Particle_Effect;
using Controller.Character.Enemy.Enemy_Base.Core;
using QFramework;
using UnityEngine;
using UnityEngine.AI;

namespace Controller.Character.Enemy.Shaman.Core
{
    public class ShamanCore : EnemyCore
    {
        private const float ClosestDistance = 3f;
        public override float ArriveDistance => 8f;

        public bool TooClose()
        {
            var remainingDistance = agent.pathPending ? float.PositiveInfinity : agent.remainingDistance;
            return ClosestDistance >= remainingDistance;
        }

        public void ShootFireball() =>
            this.SendCommand(new EnemyFireballSpawnCommand(enemyTrans,
                enemyTrans.position + 0.5f * enemyTrans.forward));
    }
}