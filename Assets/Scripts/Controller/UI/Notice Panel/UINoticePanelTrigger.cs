using System;
using System.Collections;
using System.Collections.Generic;
using Architecture;
using Extension;
using Model.Player;
using QFramework;
using QFramework.Example;
using Scriptable_Object;
using UnityEngine;

public class UINoticePanelTrigger : MonoBehaviour, IController
{
    public NoticeScriptableObject notice;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareLayer(MemoirOfWarAsset.PlayerLayer))
        {
            UIKit.OpenPanel<UINoticePanel>(new UINoticePanelData()
            {
                title = notice.title,
                content = notice.content,
                cameraController = this.GetModel<IPlayerModel>().ThirdPersonCameraController
            });
            
            gameObject.SetActive(false);
        }
    }

    public IArchitecture GetArchitecture()
    {
        return MemoirOfWar.Interface;
    }
}
