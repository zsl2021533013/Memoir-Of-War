using Controller.Character.Enemy.Skeleton.Skeleton_State.Base_State;

namespace Controller.Character.Enemy.Skeleton.Skeleton_State.Ground_State
{
    public class SkeletonDeathState : SkeletonState
    {
        public SkeletonDeathState(string animationName) : base(animationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            isStateOver = true;
            
            controller.CloseWeapon();
        }

        public override void OnExit()
        {
            base.OnExit();

            core.ResetAllExternalInput();
        }
    }
}