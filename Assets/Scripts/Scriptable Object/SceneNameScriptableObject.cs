using UnityEngine;

namespace Scriptable_Object
{
    [CreateAssetMenu(fileName = "Scene Name", menuName = "Scriptable Object/Scene Name")]
    public class SceneNameScriptableObject : ScriptableObject
    {
        public string mainTitle;
        public string subTitle;
    }
}