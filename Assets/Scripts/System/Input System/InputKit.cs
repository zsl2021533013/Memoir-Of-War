using Architecture;
using DG.Tweening;
using QFramework;
using UnityEngine;

namespace System.Input_System
{
    public enum Direction
    {
        Forward,
        Backward,
        Left,
        Right
    }
    
    public class InputKit : Singleton<InputKit>
    {
        private InputControls mInputControls;

        #region Input

        public bool Attack => mIsInputEnable && mAttack;
        public bool Defence => mIsInputEnable && mIsDefenceEnable && mDefence;
        public bool Dodge => mIsInputEnable && mDodge;
        public Vector2 Movement => (mIsMovementEnable && mIsInputEnable) ? mMovement : Vector2.zero;

        private bool mAttack;
        private bool mDefence;
        private bool mDodge;
        private Vector2 mMovement;
        
        private bool mIsInputEnable = true;
        private bool mIsMovementEnable = true;
        private bool mIsDefenceEnable = true;
        
        private Sequence mSequence;
        
        #endregion

        private InputKit()
        {
            
        }
        
        public override void OnSingletonInit()
        {
            mInputControls = new InputControls();
            
            mInputControls.Enable();

            mInputControls.Input.Move.performed += ctx => mMovement =  ctx.ReadValue<Vector2>();
            mInputControls.Input.Move.canceled += ctx => mMovement = Vector2.zero;
            
            mInputControls.Input.Attack.started += ctx => mAttack = true;
            mInputControls.Input.Attack.canceled += ctx => mAttack = false;
            
            mInputControls.Input.Defence.started += ctx => mDefence = true;
            mInputControls.Input.Defence.canceled += ctx => mDefence = false;
            
            mInputControls.Input.Dodge.started += ctx => mDodge = true;
            mInputControls.Input.Dodge.canceled += ctx => mDodge = false;
            
            ResetInput();
        }

        public InputKit ResetInput()
        {
            mAttack = false;
            mDefence = false;
            mDodge = false;
            return this;
        }
        
        public InputKit EnableInput()
        {
            mIsInputEnable = true;
            return this;
        }

        public InputKit DisableInput()
        {
            mIsInputEnable = false;
            return this;
        }

        public InputKit EnableMovement()
        {
            mIsMovementEnable = true;
            return this;
        }
        
        public InputKit DisableMovement()
        {
            mIsMovementEnable = false;
            return this;
        }

        public InputKit EnableDefence()
        {
            mSequence.Kill();
            mIsDefenceEnable = true;
            return this;
        }
        
        public InputKit DisableDefence()
        {
            mSequence.Kill(); // 避免异步交错导致的Bug
            mIsDefenceEnable = false;
            return this;
        }
        
        public InputKit DisableDefenceForSeconds(float time)
        {
            mSequence = DOTween.Sequence()
                .AppendCallback(() => mIsDefenceEnable = false)
                .AppendInterval(MemoirOfWarAsset.PlayerDefenceInterval)
                .AppendCallback(() => mIsDefenceEnable = true);
            return this;
        }
    }
}
