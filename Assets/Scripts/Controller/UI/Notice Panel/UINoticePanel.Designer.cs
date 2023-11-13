using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:aad54491-8abf-4e04-a618-2cd52e3f3323
	public partial class UINoticePanel
	{
		public const string Name = "UINoticePanel";
		
		[SerializeField]
		public UnityEngine.CanvasGroup CanvasGroup;
		[SerializeField]
		public UnityEngine.UI.Button ConfirmBtn;
		[SerializeField]
		public TMPro.TextMeshProUGUI Content;
		[SerializeField]
		public TMPro.TextMeshProUGUI Title;
		
		private UINoticePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			CanvasGroup = null;
			ConfirmBtn = null;
			Content = null;
			Title = null;
			
			mData = null;
		}
		
		public UINoticePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UINoticePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UINoticePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
