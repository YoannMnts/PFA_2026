using Naussilus.Core.Scripts.Conditions;
using Naussilus.Core.Scripts.VisualNovels.EventDatas.DialogueDatas.Answers;
using Naussilus.Core.Scripts.VisualNovels.EventDatas.DialogueDatas.DialogueLines;
using UnityEngine;

namespace Naussilus.Core.Scripts.VisualNovels.EventDatas.DialogueDatas
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class DialogueData : ScriptableObject
    {
        [field : SerializeField]
        public ComplexCondition[] Conditions { get; private set; }
        
        [field : SerializeField]
        public DialogueLine Lines { get; private set; }
        
        [field : SerializeField]
        public IAnswer[] Answers { get; private set; }
    }
}