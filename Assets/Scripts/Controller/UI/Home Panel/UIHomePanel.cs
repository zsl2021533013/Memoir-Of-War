using System.Input_System;
using Architecture;
using Cinemachine;
using Controller.Timeline;
using DG.Tweening;
using Extension;
using UnityEngine;

namespace QFramework.Example
{
	public class UIHomePanelData : UIPanelData
	{
		public CinemachineVirtualCameraBase StartCamera;
		public Transform Entrance;
	}
	
	public partial class UIHomePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIHomePanelData ?? new UIHomePanelData();

			GameStartPanel.GameStartBtn.onClick.AddListener(() =>
			{
				if (mData.Entrance.eulerAngles == MemoirOfWarAsset.EntranceLevel0Rotation)
				{
					return;
				}
				
				CanvasGroup.DOFade(0f, MemoirOfWarAsset.UIFadeDuration).OnComplete(() =>
				{
					CloseSelf();
					DirectorController.Instance.LoadAsset<GameStartTimeline>();

					var entrance = mData.Entrance;
					var pos = entrance.position;
					DOTween.Sequence()
						.AppendInterval(5f)
						.Append(entrance.DOMove(pos + Vector3.up, MemoirOfWarAsset.EntranceDuration))
						.Append(entrance.DORotate(MemoirOfWarAsset.EntranceLevel0Rotation, MemoirOfWarAsset.EntranceDuration))
						.Append(entrance.DOMove(pos, MemoirOfWarAsset.EntranceDuration));
				});
			});
			
			LevelSelectorPanel.PrevLevelBtn.onClick.AddListener(() =>
			{
				GameStartPanel.GameStartBtn.enabled = false;
				LevelSelectorPanel.PrevLevelBtn.enabled = false;
				LevelSelectorPanel.NextLevelBtn.enabled = false;
				
				DOTween.Sequence()
					.Append(GameStartPanel.CanvasGroup.DOFade(0, MemoirOfWarAsset.UIFadeDuration))
					.Join(LevelSelectorPanel.CanvasGroup.DOFade(0, MemoirOfWarAsset.UIFadeDuration))
					.OnComplete(() => 
					{
						GameStartPanel.GameStartBtn.Select();
						
						LevelSelectorPanel.CurrentLevel.text = LevelSelectorPanel.CurrentLevel.text switch
						{
							"<rotate=270>第<color=red>一</color>章</rotate>" => "<rotate=270>第<color=red>三</color>章</rotate>",
							"<rotate=270>第<color=red>二</color>章</rotate>" => "<rotate=270>第<color=red>一</color>章</rotate>",
							"<rotate=270>第<color=red>三</color>章</rotate>" => "<rotate=270>第<color=red>二</color>章</rotate>",
							_ => "<rotate=270>第<color=red>一</color>章</rotate>"
						};

						MoveAndRotateEntrance().OnComplete(() =>
						{
							DOTween.Sequence()
								.Append(GameStartPanel.CanvasGroup.DOFade(1, MemoirOfWarAsset.UIFadeDuration))
								.Join(LevelSelectorPanel.CanvasGroup.DOFade(1, MemoirOfWarAsset.UIFadeDuration))
								.OnComplete(() =>
								{
									GameStartPanel.GameStartBtn.enabled = true;
									LevelSelectorPanel.PrevLevelBtn.enabled = true;
									LevelSelectorPanel.NextLevelBtn.enabled = true;
								});
						});
					});
			});

			LevelSelectorPanel.NextLevelBtn.onClick.AddListener(() =>
			{
				GameStartPanel.GameStartBtn.enabled = false;
				LevelSelectorPanel.PrevLevelBtn.enabled = false;
				LevelSelectorPanel.NextLevelBtn.enabled = false;
				
				DOTween.Sequence()
					.Append(GameStartPanel.CanvasGroup.DOFade(0, MemoirOfWarAsset.UIFadeDuration))
					.Join(LevelSelectorPanel.CanvasGroup.DOFade(0, MemoirOfWarAsset.UIFadeDuration))
					.OnComplete(() =>
					{
						GameStartPanel.GameStartBtn.Select();
						
						LevelSelectorPanel.CurrentLevel.text = LevelSelectorPanel.CurrentLevel.text switch
						{
							"<rotate=270>第<color=red>一</color>章</rotate>" => "<rotate=270>第<color=red>二</color>章</rotate>",
							"<rotate=270>第<color=red>二</color>章</rotate>" => "<rotate=270>第<color=red>三</color>章</rotate>",
							"<rotate=270>第<color=red>三</color>章</rotate>" => "<rotate=270>第<color=red>一</color>章</rotate>",
							_ => "<rotate=270>第<color=red>一</color>章</rotate>"
						};

						AudioKit.PlaySound(SoundEffectType.EntranceMove.ToString().InsertSpace());
						
						MoveAndRotateEntrance().OnComplete(() =>
						{
							DOTween.Sequence()
								.Append(GameStartPanel.CanvasGroup.DOFade(1, MemoirOfWarAsset.UIFadeDuration))
								.Join(LevelSelectorPanel.CanvasGroup.DOFade(1, MemoirOfWarAsset.UIFadeDuration))
								.OnComplete(() =>
								{
									GameStartPanel.GameStartBtn.enabled = true;
									LevelSelectorPanel.PrevLevelBtn.enabled = true;
									LevelSelectorPanel.NextLevelBtn.enabled = true;
								});
						});
					});
			});
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

			mData.StartCamera?.IncreasePriority();

			#endregion

			#region Component

			GameStartPanel.GameStartBtn.Select();

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
			mData?.StartCamera.ResetPriority();
			
			#region Cursor

			Cursor.lockState = CursorLockMode.Locked;

			#endregion
		}

		private DG.Tweening.Sequence MoveAndRotateEntrance()
		{
			var tmp = DOTween.Sequence(); 
			
			var entrance = mData.Entrance;
			var pos = entrance.position;

			tmp.Append(entrance.DOMove(pos + Vector3.up, MemoirOfWarAsset.EntranceDuration));
			switch (LevelSelectorPanel.CurrentLevel.text)
			{
				case "<rotate=270>第<color=red>一</color>章</rotate>":
					tmp.Append(entrance.DORotate(MemoirOfWarAsset.EntranceLevel1Rotation, MemoirOfWarAsset.EntranceDuration));
					break;
				case "<rotate=270>第<color=red>二</color>章</rotate>":
					tmp.Append(entrance.DORotate(MemoirOfWarAsset.EntranceLevel2Rotation, MemoirOfWarAsset.EntranceDuration));
					break;
				case "<rotate=270>第<color=red>三</color>章</rotate>":
					tmp.Append(entrance.DORotate(MemoirOfWarAsset.EntranceLevel3Rotation, MemoirOfWarAsset.EntranceDuration));
					break;
				default:
					tmp.Append(entrance.DORotate(MemoirOfWarAsset.EntranceLevel0Rotation, MemoirOfWarAsset.EntranceDuration));
					break;
			}
			tmp.Append(entrance.DOMove(pos, MemoirOfWarAsset.EntranceDuration));

			return tmp;
		}
	}
}
