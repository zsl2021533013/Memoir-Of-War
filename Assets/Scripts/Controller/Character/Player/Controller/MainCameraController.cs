using System;
using Architecture;
using QFramework;
using UnityEngine;

namespace Controller.Character.Player.Controller
{
    public class MainCameraController : MonoBehaviour, IController
    {
        private void Awake()
        {
            transform.Parent(null); // 解除绑定
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}