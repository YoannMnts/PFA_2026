using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [CreateAssetMenu(fileName = "FinalAnswerData", menuName = "Answer/FinalAnswer", order = 0)]
    public class FinalAnswerData : AnswerData
    {
        [field : SerializeField]
        public Dependencies Dependencies { get; private set; }
        [field : SerializeField]
        public Consequences Consequences { get; private set; }
    }
}