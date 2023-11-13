using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:879ce90a-2a51-495c-af9e-7b305cad55d9
	public partial class UIDialoguePanel
	{
		public const string Name = "UIDialoguePanel";
		
		[SerializeField]
		public UnityEngine.CanvasGroup CanvasGroup;
		[SerializeField]
		public TMPro.TextMeshProUGUI Content;
		
		private UIDialoguePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			CanvasGroup = null;
			Content = null;
			
			mData = null;
		}
		
		public UIDialoguePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIDialoguePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIDialoguePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
