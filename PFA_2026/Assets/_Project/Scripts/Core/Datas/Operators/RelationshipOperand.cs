using System;
using Naussilus.Core.Datas.Conditions;
using Naussilus.Core.Datas.EStats;
using Naussilus.Core.Datas.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.DatasOperators
{
    [Serializable]
    public struct RelationshipOperand : IOperand
    {
        [field: SerializeField]
        public Relationship Npc1 { get; private set; }
        
        [field: SerializeField]
        public Relationship Npc2 { get; private set; }

        [field: SerializeReference, SubclassSelector,HideInInspector]
        public ITarget Target { get; private set; }
    }
}