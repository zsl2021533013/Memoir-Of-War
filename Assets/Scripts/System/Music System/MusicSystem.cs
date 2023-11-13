using QFramework;

namespace System.Music_System
{
    public interface IMusicSystem : ISystem
    {
    }
    
    public class MusicSystem : AbstractSystem, IMusicSystem
    {
        protected override void OnInit()
        {
            /*this.RegisterEvent<DefenceSuccessEvent>(e =>
            {
                AudioKit.PlaySound("Parry");
            });*/
        }
    }
}