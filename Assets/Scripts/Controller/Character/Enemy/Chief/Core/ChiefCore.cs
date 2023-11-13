using Controller.Character.Enemy.Enemy_Base.Core;

namespace Controller.Character.Enemy.Chief.Core
{
    public class ChiefCore : EnemyCore
    {
        public override float ArriveDistance => 1.5f;

        public void Attack2End()
        {
            animator.SetTrigger("Attack 2 End");
        }
        
        public void EnableNavMeshAgentPosition() => agent.updatePosition = true;
        public void DisableNavMeshAgentPosition() => agent.updatePosition = false;
    }
}