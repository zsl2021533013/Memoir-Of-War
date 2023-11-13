using System;
using Architecture;
using Extension;
using QFramework;
using UnityEngine;

namespace Controller.Music
{
    public class MusicTrigger : MonoBehaviour, IController
    {
        public BGMType type;

        private bool mIsUsed = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareLayer(MemoirOfWarAsset.PlayerLayer) && !mIsUsed)
            {
                AudioKit.PlayMusicWithTransition(
                    type.ToString().InsertSpace(),
                    MemoirOfWarAsset.BGMTranslationDuration
                );
                mIsUsed = true;
            }
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}