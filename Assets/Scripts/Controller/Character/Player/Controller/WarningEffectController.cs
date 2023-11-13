using Architecture;
using DG.Tweening;
using QFramework;
using UnityEngine;

namespace Controller.Character.Player.Controller
{
    public class WarningEffectController : MonoBehaviour, IController
    {
        private ParticleSystem mParticle;
        private ParticleSystem.MainModule mMainModule;

        private Tween mTween;

        private void OnEnable() // 开启一段时间后后自动关闭，写死 
        {
            mTween.Kill();
            mTween = DOVirtual.DelayedCall(MemoirOfWarAsset.WarningTime, mParticle.Stop);
        }

        private void Awake()
        {
            mParticle = GetComponentInChildren<ParticleSystem>();
            mMainModule = mParticle.main;
            mMainModule.loop = false;
        }
    
        public void Play() => mParticle.Play();
    
        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}
