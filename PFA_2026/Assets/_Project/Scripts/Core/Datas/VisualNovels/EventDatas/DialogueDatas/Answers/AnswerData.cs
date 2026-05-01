using UnityEngine;

namespace Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers
{
    public abstract class AnswerData : ScriptableObject
    {
        [field: SerializeField, TextArea]
        public string ButtonText { get; private set; }
    }
}