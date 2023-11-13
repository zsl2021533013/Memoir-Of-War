using Model.Player;
using QFramework;

namespace Command.Timeline
{
    public class PlayerStopMoveForwardCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var controller = this.GetModel<IPlayerModel>().Controller;

            controller.DisableTimeline();
        }
    }
}