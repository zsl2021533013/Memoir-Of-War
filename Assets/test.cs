using System;
using System.Collections.Generic;
using Architecture;
using Cinemachine;
using Command.Particle_Effect;
using Controller.Timeline;
using DG.Tweening;
using Extension;
using Model.Player;
using QFramework;
using QFramework.Example;
using Scriptable_Object;
using UnityEngine;
using UnityEngine.Playables;

public class Test : MonoBehaviour, IController
{
    //public List<DialogueScriptableObject> dialogues = new List<DialogueScriptableObject>();
    public string text;
    public TMPro.TextMeshProUGUI textMeshPro;
    private ResLoader mLoader = ResLoader.Allocate();
    
    private void Start()
    {
        UIKit.OpenPanel<UIHomePanel>(new UIHomePanelData()
        {
            StartCamera = GameObject.Find("Start Camera").GetComponent<CinemachineVirtualCameraBase>(),
            Entrance = GameObject.Find("Entrance").transform
        });
        AudioKit.PlayMusic(BGMType.MainTheme.ToString().InsertSpace());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            /*/*UIKit.OpenPanel<UISceneNamePanel>(new UISceneNamePanelData()
            {
                mainTitle = "帕伊城",
                subTitle = "孤城遥望紫微星"
            });#1#
            UIKit.ClosePanel<UIDialoguePanel>();
            UIKit.OpenPanel<UIDialoguePanel>(new UIDialoguePanelData()
            {
                dialogues = dialogues
            });*/
        }
    }

    public IArchitecture GetArchitecture()
    {
        return MemoirOfWar.Interface;
    }
}

