/****************************************************************************
 * 2023.4 DESKTOP-GQBECKU
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class LevelSelectorPanel
	{
		[SerializeField] public UnityEngine.CanvasGroup CanvasGroup;
		[SerializeField] public UnityEngine.UI.Button PrevLevelBtn;
		[SerializeField] public UnityEngine.UI.Button NextLevelBtn;
		[SerializeField] public TMPro.TextMeshProUGUI CurrentLevel;

		public void Clear()
		{
			CanvasGroup = null;
			PrevLevelBtn = null;
			NextLevelBtn = null;
			CurrentLevel = null;
		}

		public override string ComponentName
		{
			get { return "LevelSelectorPanel";}
		}
	}
}
