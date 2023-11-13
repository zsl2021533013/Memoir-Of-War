using System;
using Architecture;
using Cinemachine;
using Extension;
using QFramework;
using QFramework.Example;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Awake()
    {
        ResKit.Init();
    }

    private void Start()
    {
        UIKit.OpenPanel<UIHomePanel>(new UIHomePanelData()
        {
            StartCamera = GameObject.Find("Start Camera").GetComponent<CinemachineVirtualCameraBase>(),
            Entrance = GameObject.Find("Entrance").transform
        });
        AudioKit.PlayMusicWithTransition(
            BGMType.MainTheme.ToString().InsertSpace(),
            MemoirOfWarAsset.BGMTranslationDuration
        );
    }
}