using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "DialogueAnswer", order = 0)]
    public class DialogueData : ScriptableObject
    {
        [field : SerializeField]
        public Lines Lines { get; private set; }
        
        [field : SerializeField]
        public AnswerData[] Answers { get; private set; }
        
        [field : SerializeField]
        public Dependencies Dependencies { get; private set; }
    }
}