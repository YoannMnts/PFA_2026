using Naussilus.Core.Datas.Conditions;
using Naussilus.Core.Datas.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Datas.VisualNovels
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class EventData : ScriptableObject
    {
        [field : SerializeField]
        public string Name { get; private set; }
        
        [field : SerializeField]
        public NpcData[] Npcs { get; private set; }
        
        [field : SerializeField]
        public int Priority { get; private set; }
        
        [field : SerializeField]
        public ComplexCondition[] Conditions { get; private set; }
        
        [field : SerializeField]
        public DialogueData FirstDialogue { get; private set; }
    }
}