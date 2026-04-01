using System;
using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [Serializable]
    public struct RoomAction
    {
        [field: SerializeField]
        public NpcData Npc { get; private set; }
        
        [field: SerializeField]
        public int CategoryIndex { get; private set; }
        
        [field: SerializeField]
        public RoomActionCondition[] Conditions { get; private set; }
        
        [field: SerializeField]
        public RoomRelationshipActionCondition[] RelationshipsConditions { get; private set; }
    }
}