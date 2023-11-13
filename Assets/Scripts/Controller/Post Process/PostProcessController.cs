using Architecture;
using DG.Tweening;
using Event.Character.Player;
using Event.Statue;
using QFramework;
using UnityEngine;
using UnityEngine.Rendering;

namespace Controller.Post_Process
{
    public class PostProcessController : MonoBehaviour, IController
    {
        private Volume mVolume;
    
        private void Awake()
        {
            mVolume = GetComponent<Volume>();

            this.RegisterEvent<BulletTimeStartEvent>(e =>
            {
                DOTween.To(() => mVolume.weight, value => mVolume.weight = value, 1f, 0.1f);
            });
        
            this.RegisterEvent<BulletTimeEndEvent>(e =>
            {
                DOTween.To(() => mVolume.weight, value => mVolume.weight = value, 0f, 0.1f);
            });
            
            this.RegisterEvent<StatueBulletTimeStartEvent>(e =>
            {
                DOTween.To(() => mVolume.weight, value => mVolume.weight = value, 1f, 0.1f);
            });
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}
