using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:eed5eedc-0aa4-488a-851f-d4830ffbf299
	public partial class UISceneNamePanel
	{
		public const string Name = "UISceneNamePanel";
		
		[SerializeField]
		public UnityEngine.CanvasGroup CanvasGroup;
		[SerializeField]
		public TMPro.TextMeshProUGUI MainTitle;
		[SerializeField]
		public TMPro.TextMeshProUGUI SubTitle;
		
		private UISceneNamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			CanvasGroup = null;
			MainTitle = null;
			SubTitle = null;
			
			mData = null;
		}
		
		public UISceneNamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UISceneNamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UISceneNamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
