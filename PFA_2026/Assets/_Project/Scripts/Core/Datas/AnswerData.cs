using UnityEngine;

namespace Naussilus.Core.Scripts
{
    public abstract class AnswerData : ScriptableObject
    {
        [field: SerializeField, TextArea] 
        public string Text { get; private set; }
    } 
}