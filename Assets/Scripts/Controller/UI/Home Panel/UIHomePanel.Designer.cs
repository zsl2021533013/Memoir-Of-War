using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:e93e4914-1c4b-4834-93c9-6ddb9ed3b72c
	public partial class UIHomePanel
	{
		public const string Name = "UIHomePanel";
		
		[SerializeField]
		public UnityEngine.CanvasGroup CanvasGroup;
		[SerializeField]
		public TMPro.TextMeshProUGUI GameTitle;
		[SerializeField]
		public GameStartPanel GameStartPanel;
		[SerializeField]
		public LevelSelectorPanel LevelSelectorPanel;
		
		private UIHomePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			CanvasGroup = null;
			GameTitle = null;
			GameStartPanel = null;
			LevelSelectorPanel = null;
			
			mData = null;
		}
		
		public UIHomePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIHomePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIHomePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
