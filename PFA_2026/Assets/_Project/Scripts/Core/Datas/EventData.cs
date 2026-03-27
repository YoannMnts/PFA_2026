using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [CreateAssetMenu(fileName = "EventData", menuName = "EventAnswer", order = 0)]
    public class EventData : ScriptableObject
    {
        [field : SerializeField]
        public int Priority { get; private set; }
        
        [field : SerializeField]
        public NpcData[] Npcs { get; private set; }
        
        [field : SerializeField]
        public Dependencies Dependencies { get; private set; }
        
        [field : SerializeField]
        public DialogueData FirstDialogue { get; private set; }
    }
}