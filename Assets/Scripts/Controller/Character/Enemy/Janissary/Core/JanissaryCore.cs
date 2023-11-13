using Command.Particle_Effect;
using Controller.Character.Enemy.Enemy_Base.Core;
using QFramework;

namespace Controller.Character.Enemy.Janissary.Core
{
    public class JanissaryCore : EnemyCore
    {
        private const float Attack3Distance = 3f;
        public override float ArriveDistance => 2f;

        public bool HasArrivedAttack4Range()
        {
            var remainingDistance = agent.pathPending ? float.PositiveInfinity : agent.remainingDistance;
            return ArriveDistance <= remainingDistance && remainingDistance <= ArriveDistance + Attack3Distance;
        }

        public void Beam() => this.SendCommand<BeamSpawnCommand>();
        // TODO: 有问题，粒子系统最好只管粒子，不应插手战斗，现在战斗检测在 BeamController 里
    }
}