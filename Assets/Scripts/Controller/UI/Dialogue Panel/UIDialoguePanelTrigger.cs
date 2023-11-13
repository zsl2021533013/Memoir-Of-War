using System.Collections.Generic;
using Architecture;
using DG.Tweening;
using Extension;
using Model.Player;
using Scriptable_Object;
using UnityEngine;

namespace QFramework.Example
{
    public class UIDialoguePanelTrigger : MonoBehaviour, IController
    {
        public List<DialogueScriptableObject> dialogues = new List<DialogueScriptableObject>();

        private bool mIsUsed = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareLayer(MemoirOfWarAsset.PlayerLayer) && !mIsUsed)
            {
                UIKit.ClosePanel<UIDialoguePanel>();
                UIKit.OpenPanel<UIDialoguePanel>(new UIDialoguePanelData()
                {
                    dialogues = dialogues
                });

                mIsUsed = true;
            }
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}