using System;
using Architecture;
using Cinemachine;
using Extension;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Controller.Character.Player.Controller
{
    public class ThirdPersonCameraController : MonoBehaviour, IController
    {
        private CinemachineFreeLook mThirdPersonCamera;
        private CinemachineInputProvider mInputProvider;

        private void Awake()
        {
            mThirdPersonCamera = GetComponent<CinemachineFreeLook>();
            mInputProvider = GetComponent<CinemachineInputProvider>();
        }

        private void Start()
        {
            var playerData = this.GetModel<IPlayerModel>();
            var playerTrans = playerData.Transform;
            var cameraPoint = playerTrans.FindAllChildren("Camera Point");

            mThirdPersonCamera.LookAt = cameraPoint;
            mThirdPersonCamera.Follow = cameraPoint;
            
            ResetPosition();
        }

        public ThirdPersonCameraController ResetPosition()
        {
            var playerData = this.GetModel<IPlayerModel>();
            var playerTrans = playerData.Transform;
            var cameraPoint = playerTrans.FindAllChildren("Camera Point").position;
            
            mThirdPersonCamera.ForceCameraPosition(cameraPoint - 3 * playerTrans.forward, 
                Quaternion.LookRotation(cameraPoint - transform.position, Vector3.up));

            return this;
        }

        public ThirdPersonCameraController SetPosition(Vector3 position, Quaternion rotation)
        {
            mThirdPersonCamera.ForceCameraPosition(position, rotation);
            return this;
        }

        public ThirdPersonCameraController EnableInput()
        {
            mInputProvider.enabled = true;
            return this;
        }
        
        public ThirdPersonCameraController DisableInput()
        {
            mInputProvider.enabled = false;
            return this;
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}