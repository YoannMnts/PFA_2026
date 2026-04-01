using System;
using Naussilus.Core.Scripts.Enums;
using PlasticGui.Help;
using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [Serializable]
    public struct RoomActionCondition
    {
        [field: SerializeField]
        public Enums.NpcStats Stat1 { get; private set; }
        
        [field: SerializeField]
        public Comparison Operator { get; private set; }
        
        [field: SerializeField, Range(0, 20)]
        public int Number { get; private set; }
        
        [field: SerializeField]
        public RoomActionEffect Effect { get; private set; }
    }
    
    [Serializable]
    public struct RoomRelationshipActionCondition
    {
        [field: SerializeField]
        public NpcStats Stat1 { get; private set; }
    
        [field: SerializeField]
        public Comparison Operator { get; private set; }
    
        [field: SerializeField]
        public RelationshipStat Stat2 { get; private set; }
        
        [field: SerializeField]
        public RoomRelationshipActionCondition[] AdditionalCondition { get; private set; }
    }

    [Serializable]
    public struct RelationshipStat
    {
        [field: SerializeField]
        public NpcData Npc { get; private set; }
        
        [field: SerializeField]
        public Enums.NpcStats NpcStats { get; private set; }
    }
}