using UnityEngine;

namespace Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers
{
    public abstract class Answer : ScriptableObject
    {
        [field: SerializeField, TextArea]
        public string PlayerText { get; private set; }
    }
}