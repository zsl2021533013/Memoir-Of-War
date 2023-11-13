using Architecture;
using Command.Portal;
using Extension;
using QFramework;
using UnityEngine;

namespace Controller.Portal
{
    public class PortalDestinationController : MonoBehaviour, IController
    {
        private PortalController mController;
        private Transform mPlayerCamera;
        private Camera mPortalCamera;

        public void Init(PortalController controller)
        {
            mController = controller;
            mPlayerCamera = Camera.main?.transform;
            mPortalCamera = transform.GetComponentInChildren<Camera>();
            
            if (mPortalCamera.targetTexture != null)
            {
                mPortalCamera.targetTexture.Release();
            }
            mPortalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            
            mController.GetPortalMaterial().mainTexture = mPortalCamera.targetTexture;
        }

        public void UpdatePortalCamera()
        {
            var m = transform.localToWorldMatrix *
                    mController.transform.worldToLocalMatrix *
                    mPlayerCamera.transform.localToWorldMatrix;
            mPortalCamera.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}