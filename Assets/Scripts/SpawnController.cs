using Architecture;
using Command.Battle;
using QFramework;
using UnityEngine;

public class SpawnController : MonoBehaviour, IController
{
    public EnemyType type;
        
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            this.SendCommand(new EnemySpawnCommand(type, Vector3.zero + Vector3.back, Quaternion.identity));
        }
    }

    public IArchitecture GetArchitecture()
    {
        return MemoirOfWar.Interface;
    }
}