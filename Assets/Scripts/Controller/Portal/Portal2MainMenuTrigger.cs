using Architecture;
using Extension;
using UnityEngine;

namespace Controller.Portal
{
    public class Portal2MainMenuTrigger : MonoBehaviour
    {
        private Portal2MainMenuController mController;

        private void Awake()
        {
            mController = transform.parent.GetComponent<Portal2MainMenuController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareLayer(MemoirOfWarAsset.PlayerLayer))
            {
                mController.Return2MainMenu();
            }
        }
    }
}
