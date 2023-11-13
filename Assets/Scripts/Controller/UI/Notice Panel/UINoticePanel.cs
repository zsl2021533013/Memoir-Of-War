using System.Input_System;
using Architecture;
using Controller.Character.Player.Controller;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Unity.VisualScripting;

namespace QFramework.Example
{
	public class UINoticePanelData : UIPanelData
	{
		public string title = "";
		public string content = "";
		public ThirdPersonCameraController cameraController;
	}
	public partial class UINoticePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UINoticePanelData ?? new UINoticePanelData();
			// please add init code here
			
			ConfirmBtn.onClick.AddListener((() =>
			{
				CanvasGroup.DOFade(0f, MemoirOfWarAsset.UIFadeDuration).OnComplete(() =>
				{
					InputKit.Instance.EnableInput();
					CloseSelf();
				});
			}));
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			#region UI Asset

			UIKit.Root.ScreenSpaceOverlayRenderMode();
			UIKit.Root.SetResolution(1920, 1080, 0.5f);

			#endregion

			#region Fade In

			CanvasGroup.alpha = 0f;
			CanvasGroup.DOFade(1f, MemoirOfWarAsset.UIFadeDuration);

			#endregion
			
			#region Cursor

			Cursor.lockState = CursorLockMode.None;

			#endregion

			#region Input

			InputKit.Instance.DisableInput();

			#endregion

			#region Camera

			mData.cameraController.DisableInput();

			#endregion
			
			#region Component

			Title.text = mData.title;
			Content.text = mData.content;
			
			ConfirmBtn.Select();

			#endregion
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
			mData.cameraController.EnableInput();
			
			#region Cursor

			Cursor.lockState = CursorLockMode.Locked;

			#endregion
		}
	}
}
