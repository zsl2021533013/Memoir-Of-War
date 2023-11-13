using System;
using System.Collections;
using System.Collections.Generic;
using Architecture;
using Command.Battle;
using Controller.Timeline;
using DG.Tweening;
using Extension;
using QFramework;
using UnityEngine;

public class Portal2MainMenuController : MonoBehaviour, IController
{
    private Transform mTrigger;

    private void Awake()
    {
        mTrigger = transform.FindAllChildren("Trigger");
    }

    public void Return2MainMenu()
    {
        DOTween.Sequence()
            .AppendCallback(() =>
                this.SendCommand(new PlayerEnforceMoveAndRotateCommand(mTrigger.position, transform.position)))
            .AppendInterval(2 * MemoirOfWarAsset.PlayerEnforceMoveOrRotateDuration)
            .AppendCallback(() => DirectorController.Instance.LoadAsset<Return2MainMenuTimeline>());
    }

    public IArchitecture GetArchitecture()
    {
        return MemoirOfWar.Interface;
    }
}
