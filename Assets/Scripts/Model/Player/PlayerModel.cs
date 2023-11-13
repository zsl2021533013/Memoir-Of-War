using Cinemachine;
using Controller.Character.Player.Controller;
using Event.Character.Player;
using Extension;
using QFramework;
using UnityEngine;

namespace Model.Player
{
    public interface IPlayerModel : IModel
    {
        public PlayerController Controller { get; }
        public WarningEffectController WarningEffectController  { get; }
        public Transform Transform  { get; }
        public Rigidbody Rigidbody  { get; }
        public Animator Animator { get;}
        public bool IsBulletTime { get; set; }
        public BindableProperty<float> Health { get; }

        #region Camera

        public CinemachineBrain CinemachineBrain { get; }
        public ThirdPersonCameraController ThirdPersonCameraController { get; }
        public CinemachineVirtualCameraBase ThirdPersonCamera { get; }
        public CinemachineVirtualCameraBase StatueCamera { get; }
        public CinemachineVirtualCameraBase PortalCamera { get; }
        public CinemachineVirtualCameraBase StartCamera { get; }

        #endregion
        
        public void RegisterPlayer(Transform transform);
        public void IncreaseHealth(float num);
        public void DecreaseHealth(float num);
    }
    
    public class PlayerModel : AbstractModel, IPlayerModel
    {
        public PlayerController Controller { get; private set; }
        public WarningEffectController WarningEffectController { get; private set; }
        public Transform Transform  { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public CinemachineBrain CinemachineBrain { get; private set; }
        public ThirdPersonCameraController ThirdPersonCameraController { get; private set; }
        public CinemachineVirtualCameraBase ThirdPersonCamera { get; private set; }
        public CinemachineVirtualCameraBase StatueCamera { get; private set; }
        public CinemachineVirtualCameraBase PortalCamera { get; private set; }
        public CinemachineVirtualCameraBase StartCamera { get; private set; }
        public bool IsBulletTime { get; set; } = false;
        public BindableProperty<float> Health { get; private set; } = new BindableProperty<float>(1000);
        
        protected override void OnInit()
        {
            Health.Register(newValue =>
            {
                if (newValue <= 0)
                {
                    this.SendEvent<PlayerDieEvent>();
                }
            });
        }

        public void RegisterPlayer(Transform transform)
        { 
            // TODO: 有点低效
            Transform = transform;
            Controller = transform.GetComponentInChildren<PlayerController>();
            WarningEffectController = transform.GetComponentInChildren<WarningEffectController>();
            Rigidbody = transform.GetComponentInChildren<Rigidbody>();
            Animator = transform.GetComponentInChildren<Animator>();

            CinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
            ThirdPersonCamera = transform.FindAllChildren("Third Person Camera")
                .GetComponent<CinemachineVirtualCameraBase>();
            StatueCamera = transform.FindAllChildren("Statue Camera")
                .GetComponentInChildren<CinemachineVirtualCameraBase>();
            PortalCamera = transform.FindAllChildren("Portal Camera")
                .GetComponentInChildren<CinemachineVirtualCameraBase>();
            StartCamera = transform.FindAllChildren("Start Camera")
                .GetComponentInChildren<CinemachineVirtualCameraBase>();
            
            ThirdPersonCameraController = ThirdPersonCamera.GetComponent<ThirdPersonCameraController>();

            Debug.Log("Player has registered");
        }


        public void IncreaseHealth(float num) => Health.Value += num;
        public void DecreaseHealth(float num) => Health.Value -= num;
        public void StartImmortal() => IsBulletTime = true;
        public void EndImmortal() => IsBulletTime = false; // TODO: 没有用到啊
    }
}