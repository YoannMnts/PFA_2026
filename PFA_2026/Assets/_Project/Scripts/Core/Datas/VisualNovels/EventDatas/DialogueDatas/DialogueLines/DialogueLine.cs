using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Core.Scripts.VisualNovels.EventDatas.DialogueDatas.DialogueLines
{
    public struct DialogueLine
    {
        [field : SerializeField]
        public NpcData Npc { get; private set; }
        
        [field : SerializeField]
        public string Text { get; private set; }
        
        [field : SerializeField, HideInInspector]
        public Image Expression { get; private set; }
    }
}