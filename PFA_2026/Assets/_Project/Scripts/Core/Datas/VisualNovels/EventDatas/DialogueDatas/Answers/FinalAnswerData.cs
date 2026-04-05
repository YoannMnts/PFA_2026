using Naussilus.Core.Datas.Conditions;
using UnityEngine;

namespace Naussilus.Core.Datas.VisualNovels
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