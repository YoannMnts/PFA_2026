using UnityEngine;

namespace Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers
{
    public abstract class Answer : ScriptableObject
    {
        [field: SerializeField]
        public string ButtonText { get; private set; }
    }
}