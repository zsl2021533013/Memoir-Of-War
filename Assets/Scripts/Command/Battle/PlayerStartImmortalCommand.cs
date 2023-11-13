using Model.Player;
using QFramework;

namespace Command.Battle
{
    public class PlayerStartImmortalCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<IPlayerModel>().IsBulletTime = true;
        }
    }
}