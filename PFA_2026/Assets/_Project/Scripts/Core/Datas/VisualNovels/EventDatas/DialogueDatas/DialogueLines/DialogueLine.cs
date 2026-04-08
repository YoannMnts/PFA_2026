using System;
using Naussilus.Core.NpcDatas;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.DialogueLines
{
    [Serializable]
    public struct DialogueLine
    {
        [field : SerializeField]
        public NpcData Npc { get; private set; }
        
        [field : SerializeField, TextArea]
        public string Text { get; private set; }
        
        [field : SerializeField, HideInInspector]
        public Image Expression { get; private set; }
    }
}