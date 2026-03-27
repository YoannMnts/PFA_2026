using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [CreateAssetMenu(fileName = "IntermediateAnswerData", menuName = "Answer/IntermediateAnswer", order = 0)]
    public class IntermediateAnswer : AnswerData
    {
        [field : SerializeField]
        public DialogueData NextDialogue { get; private set; }
    }
}