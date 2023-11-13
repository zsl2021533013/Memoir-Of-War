using Architecture;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class UISceneNamePanelData : UIPanelData
	{
		public string mainTitle;
		public string subTitle;
	}
	
	public partial class UISceneNamePanel : UIPanel
	{
		private const float MainTitleMaxMSpaceValue = 120f;
		private const float MainTitleInitMSpaceValue = 80f;
		private const float SubTitleMaxMSpaceValue = 60f;
		private const float SubTitleInitMSpaceValue = 36f;
		
		private float mCurrentMainTitleMSpaceValue;
		private float mCurrentSubTitleMSpaceValue;
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UISceneNamePanelData ?? new UISceneNamePanelData();
			// please add init code here
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			#region UI Asset

			UIKit.Root.ScreenSpaceOverlayRenderMode();
			UIKit.Root.SetResolution(1920, 1080, 0.5f);

			#endregion
			
			mCurrentMainTitleMSpaceValue = MainTitleInitMSpaceValue;
			mCurrentSubTitleMSpaceValue = SubTitleInitMSpaceValue;

			DOTween.Sequence()
				.AppendCallback(() => CanvasGroup.alpha = 0f)
				.Append(CanvasGroup.DOFade(1f, MemoirOfWarAsset.UIFadeDuration))
				.AppendInterval(MemoirOfWarAsset.SceneNameShowTime)
				.Append(CanvasGroup.DOFade(0f, MemoirOfWarAsset.UIFadeDuration))
				.AppendCallback(CloseSelf);
			
			DOTween.To(() => mCurrentMainTitleMSpaceValue,
				value =>
				{
					mCurrentMainTitleMSpaceValue = value;
					MainTitle.text = $"<mspace={mCurrentMainTitleMSpaceValue}>{mData.mainTitle}</mspace>";
				},
				MainTitleMaxMSpaceValue,
				MemoirOfWarAsset.SceneNameDuration);
			
			DOTween.To(() => mCurrentSubTitleMSpaceValue,
				value =>
				{
					mCurrentSubTitleMSpaceValue = value;
					SubTitle.text = $"<mspace={mCurrentSubTitleMSpaceValue}>{mData.subTitle}</mspace>";
				},
				SubTitleMaxMSpaceValue,
				MemoirOfWarAsset.SceneNameDuration);
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
