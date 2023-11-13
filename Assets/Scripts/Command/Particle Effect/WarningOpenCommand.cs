using Model.Player;
using QFramework;
using UnityEngine;

namespace Command.Particle_Effect
{
    public class WarningOpenCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            Debug.Log("Warning");
            
            this.GetModel<IPlayerModel>()
                .WarningEffectController.Play();
        }
    }
}