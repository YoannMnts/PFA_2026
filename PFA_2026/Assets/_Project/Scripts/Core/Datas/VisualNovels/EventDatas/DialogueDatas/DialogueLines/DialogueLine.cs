using Naussilus.Core.Datas.NpcDatas;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Core.Datas.VisualNovels
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