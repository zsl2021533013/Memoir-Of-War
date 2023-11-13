using System.Collections.Generic;
using System.Input_System;
using Architecture;
using Command.Battle;
using Command.Statue;
using DG.Tweening;
using Event.Statue;
using Extension;
using Model.Player;
using QFramework;
using UnityEngine;

namespace Controller.Statue
{
    public enum StatueAttackType
    {
        Normal,
        ShieldBreak
    }

    public class StatueController : MonoBehaviour, IController
    {
        public List<StatueAttackType> attackSequence;
        
        private BlessController mBlessController;
        
        private List<Material> mMaterials = new List<Material>();
        
        private Collider mCollider;

        private BindableProperty<bool> mIsStatueInteract = new BindableProperty<bool>(false);
        /*
         表示雕像是否与玩家互动
         当多个雕像开启时
         让 Statue Attack Success/Fail Event 只与与玩家互动的那个雕像交互
         同时通过数值驱动改变 Bless 特效的开闭
         */
        
        private int mSequenceIndex = 0;

        private bool mIsStatueEnable = true;
        /*
         表示雕像是否开启
         当雕像关闭时，停止一切检测和交互
         */

        private void Awake()
        {
            InitComponent();
            
            RegisterEvent(); // 注册事件
        }

        private void FixedUpdate()
        {
            if (DetectPlayer() && !mIsStatueInteract && mIsStatueEnable)
            {
                mIsStatueInteract.Value = true;
                
                this.SendCommand<PlayerConstrainCommand>();
                
                this.SendCommand(new PlayerEnforceMoveAndRotateCommand(
                    transform.position + MemoirOfWarAsset.StatuePlayerOffset * transform.forward, 
                    transform.position));
                
                DOVirtual.DelayedCall(1.5f, () =>
                {
                    if (attackSequence.Count == 0)
                    {
                        Debug.LogError("Attack sequence of this statue is empty !");
                    }
                    else
                    {
                        this.SendCommand(new StatueAttackCommand(transform, attackSequence[0]));
                    }
                });
            }
        }

        private void InitComponent()
        {
            mBlessController = GetComponentInChildren<BlessController>();
            
            var meshRenders = GetComponentsInChildren<Renderer>();
            foreach (var meshRender in meshRenders)
            {
                mMaterials.AddRange(meshRender.materials);
            }
                        
            mCollider = GetComponent<Collider>();

            mIsStatueInteract.Register(value =>
            {
                if (value)
                {
                    mBlessController.Stop(); 
                }
                else
                {
                    mBlessController.Play();
                }
            });
        }

        private void RegisterEvent()
        {
            this.RegisterEvent<StatueAttackSuccessEvent>(OnStatueAttackSuccess);
            
            this.RegisterEvent<StatueAttackFailEvent>(OnStatueAttackFail);
        }
        
        private void UnregisterEvent()
        {
            this.UnRegisterEvent<StatueAttackSuccessEvent>(OnStatueAttackSuccess);
            
            this.UnRegisterEvent<StatueAttackFailEvent>(OnStatueAttackFail);
        }

        private bool DetectPlayer() => 
            Physics.CheckSphere(
                transform.position + MemoirOfWarAsset.StatueTriggerOffset * transform.forward, 
                MemoirOfWarAsset.StatueDetectRadius, 
                MemoirOfWarAsset.PlayerLayerMask);

        private void Dissolve()
        {
            mCollider.enabled = false;
            foreach (var mat in mMaterials)
            {
                mat.SetFloat("_EffectValue", 0f);
            }
            foreach (var mat in mMaterials)
            {
                mat.DOFloat(1, "_EffectValue",  MemoirOfWarAsset.DissolveTime)
                    .OnComplete(() => gameObject.SetActive(false));
            }
        }

        private void OnStatueAttackSuccess(StatueAttackSuccessEvent e)
        {
            if (!mIsStatueInteract) return;

            this.SendCommand<PlayerFreeCommand>();
                
            mSequenceIndex = 0;
            mIsStatueInteract.Value = false;
        }
        
        private void OnStatueAttackFail(StatueAttackFailEvent e)
        {
            if (!mIsStatueInteract) return;
                
            if (mSequenceIndex == attackSequence.Count - 1) // 所有攻击均已格挡或闪避
            {
                UnregisterEvent(); // 解除事件的注册
                
                this.SendCommand<PlayerFreeCommand>();
                    
                /*mSequenceIndex = 0;
                mIsStatueActive.Value = false;*/

                mIsStatueEnable = false;
                mIsStatueInteract.SetValueWithoutEvent(false); // 不再与玩家互动，但不打开特效
                    
                DOVirtual.DelayedCall(1f, Dissolve);

                return;
            }
                
            DOVirtual.DelayedCall(MemoirOfWarAsset.StatueAttackInterval, () =>
            {
                mSequenceIndex++;
                this.SendCommand(new StatueAttackCommand(transform, attackSequence[mSequenceIndex]));
            });
        }

        public IArchitecture GetArchitecture()
        {
            return MemoirOfWar.Interface;
        }
    }
}