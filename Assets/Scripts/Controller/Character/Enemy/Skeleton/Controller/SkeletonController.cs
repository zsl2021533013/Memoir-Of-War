using Controller.Character.Enemy.Enemy_Base.Controller;
using Controller.Character.Enemy.Enemy_Base.Core;
using Controller.Character.Enemy.Enemy_Base.State;
using Controller.Character.Enemy.Skeleton.Core;
using Controller.Character.Enemy.Skeleton.Skeleton_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Ground_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Skill_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Stabbed_State;
using Controller.Character.Enemy.Skeleton.Skeleton_State.Turn_State;

namespace Controller.Character.Enemy.Skeleton.Controller
{
    public class SkeletonController : EnemyController 
    {
        protected override EnemyState StartState => GetState<SkeletonIdleState>();
        protected override EnemyCore InitCore => new SkeletonCore();

        protected override void InitializeFSM()
        {
            RegisterState(new SkeletonIdleState("Idle"));
            RegisterState(new SkeletonChaseState("Chase"));
            RegisterState(new SkeletonDeathState("Death"));
            RegisterState(new SkeletonWalkAroundState("Walk Around"));
            
            RegisterState(new SkeletonAttack1State("Attack 1"));
            RegisterState(new SkeletonAttack2State("Attack 2"));
            
            RegisterState(new SkeletonShieldBreakState("Shield Break"));
            RegisterState(new SkeletonStabbedDeathSate("Stabbed Death"));
            RegisterState(new SkeletonStabbedRaiseState("Stabbed Raise"));
            RegisterState(new SkeletonStabbedState("Stabbed"));
            
            RegisterState(new SkeletonTurnLeftState("Turn Left"));
            RegisterState(new SkeletonTurnRightState("Turn Right"));
            RegisterState(new SkeletonTurnBackState("Turn Back"));
        }
    }
}
