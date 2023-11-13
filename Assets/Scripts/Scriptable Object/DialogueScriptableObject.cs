using System;
using Architecture;
using UnityEngine;
using UnityEngine.UI;

namespace Scriptable_Object
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Object/Dialogue")]
    public class DialogueScriptableObject : ScriptableObject
    {
        public DialogueCharacterType type;
        public string dialogueText;

        public string Text
        {
            get
            {
                var tmp = "";
                tmp += type switch
                {
                    DialogueCharacterType.Xin => "<color=#FF3300>心</color>：",
                    DialogueCharacterType.WuShang => "<color=#87CEEB>武尚</color>：",
                    _ => throw new ArgumentOutOfRangeException()
                };

                tmp += dialogueText;

                return tmp;
            }
        }
    }
}