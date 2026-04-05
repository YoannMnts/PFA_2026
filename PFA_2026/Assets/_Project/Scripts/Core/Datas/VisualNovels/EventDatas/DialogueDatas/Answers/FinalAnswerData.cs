using Naussilus.Core.Scripts.Conditions;
using Naussilus.Core.Scripts.VisualNovels.EventDatas.DialogueDatas.Answers.EventConsequences;
using UnityEngine;

namespace Naussilus.Core.Scripts.VisualNovels.EventDatas.DialogueDatas.Answers
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class FinalAnswerData : ScriptableObject
    {
        [field : SerializeField]
        public ComplexCondition[] Conditions { get; private set; }
        
        [field : SerializeField]
        public EventConsequence Consequence { get; private set; }
    }
}