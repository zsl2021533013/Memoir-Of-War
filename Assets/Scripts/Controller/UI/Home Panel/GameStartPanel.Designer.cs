/****************************************************************************
 * 2023.4 DESKTOP-GQBECKU
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class GameStartPanel
	{
		[SerializeField] public UnityEngine.CanvasGroup CanvasGroup;
		[SerializeField] public UnityEngine.UI.Button GameStartBtn;

		public void Clear()
		{
			CanvasGroup = null;
			GameStartBtn = null;
		}

		public override string ComponentName
		{
			get { return "GameStartPanel";}
		}
	}
}
