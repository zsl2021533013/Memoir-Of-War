using Architecture;
using Extension;
using UnityEngine;

namespace Controller.Battlefield
{
    public class BattlefieldTrigger : MonoBehaviour
    {
        private BattlefieldController mController;
        
        private bool mIsUsed = false;

        private void Awake()
        {
            mController = transform.parent.GetComponent<BattlefieldController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareLayer(MemoirOfWarAsset.PlayerLayer) && !mIsUsed)
            {
                mController.SpawnEnemy();
                mIsUsed = true;
            }
        }
    }
}