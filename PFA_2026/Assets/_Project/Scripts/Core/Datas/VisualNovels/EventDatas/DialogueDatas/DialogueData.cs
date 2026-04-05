using Naussilus.Core.Datas.Conditions;
using UnityEngine;

namespace Naussilus.Core.Datas.VisualNovels
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