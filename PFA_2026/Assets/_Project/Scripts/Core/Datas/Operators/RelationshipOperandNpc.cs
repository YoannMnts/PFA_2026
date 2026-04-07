using System;
using Naussilus.Core.Datas.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.DatasOperators
{
    [Serializable]
    public struct RelationshipOperandNpc : IOperand
    {
        [field : SerializeField]
        public NpcData Npc1 { get; private set; }
        
        [field : SerializeField]
        public NpcData Npc2 { get; private set; }
    }
}