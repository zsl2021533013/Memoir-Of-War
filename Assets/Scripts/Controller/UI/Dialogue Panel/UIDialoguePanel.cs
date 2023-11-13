using System.Collections.Generic;
using System.Text;
using Architecture;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Extension;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Scriptable_Object;

namespace QFramework.Example
{
	public class UIDialoguePanelData : UIPanelData
	{
		public List<DialogueScriptableObject> dialogues = new List<DialogueScriptableObject>();
	}
	public partial class UIDialoguePanel : UIPanel
	{
		private Sequence mSequence;
		private TweenerCore<string, string, StringOptions> mTextTween;
		private TweenerCore<float, float, FloatOptions> mCanvasTween;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIDialoguePanelData ?? new UIDialoguePanelData();
			// please add init code here
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			#region Fade In

			CanvasGroup.alpha = 0f;
			mCanvasTween = CanvasGroup.DOFade(1f, MemoirOfWarAsset.UIFadeDuration);

			#endregion

			#region Component

			ShowDialogue(0, mData.dialogues);

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
			mCanvasTween?.Kill();
			mTextTween?.Kill();
			mSequence?.Kill();
		}

		private void ShowDialogue(int index, List<DialogueScriptableObject> dialogues)
		{
			if (index >= dialogues.Count)
			{
				DOTween.Sequence() // 对话放完了，关闭
					.Append(mCanvasTween = CanvasGroup.DOFade(0f, MemoirOfWarAsset.UIFadeDuration))
					.OnComplete(CloseSelf);
				return;
			}
			
			var text = dialogues[index].Text;
			Content.text = "";
			mTextTween = Content.DOText(text, text.Length * MemoirOfWarAsset.PerCharDuration)
				.SetEase(Ease.Linear)
				.OnComplete(() =>
				{
					mSequence = DOTween.Sequence()
						.AppendInterval(MemoirOfWarAsset.PerDialogueDelay)
						.AppendCallback(() => ShowDialogue(index + 1, dialogues));
				});
		}
	}
}
