using UnityEngine;

namespace Naussilus.Core.Scripts.VisualNovels.EventDatas.DialogueDatas.Answers
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class BasicAnswerData : ScriptableObject, IAnswer
    {
        [field : SerializeField]
        public DialogueData NextDialogue { get; private set; }
    }
}