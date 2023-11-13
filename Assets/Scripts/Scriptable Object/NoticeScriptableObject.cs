using UnityEngine;

namespace Scriptable_Object
{
    [CreateAssetMenu(fileName = "Notice", menuName = "Scriptable Object/Notice")]
    public class NoticeScriptableObject : ScriptableObject
    {
        public string title;
        public string content;
    }
}
