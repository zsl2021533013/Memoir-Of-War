using Model.Player;
using QFramework;

namespace Command.Battle
{
    public class PlayerEndImmortalCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<IPlayerModel>().IsBulletTime = false;
        }
    }
}