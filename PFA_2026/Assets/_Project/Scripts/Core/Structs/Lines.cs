using System;
using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [Serializable]
    public struct Lines
    {
        [field: SerializeField, TextArea]
        public string Text { get; private set; }
        
        [field: SerializeField]
        public NpcData Npc { get; private set; }
        
        [field: SerializeField]
        public Sprite Sprite { get; private set; }
    }
}