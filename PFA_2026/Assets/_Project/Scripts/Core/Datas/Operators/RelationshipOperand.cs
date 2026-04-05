using Naussilus.Core.Datas.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.DatasOperators
{
    public struct RelationshipOperand
    {
        [field : SerializeField]
        public NpcData Npc1 { get; private set; }
        
        [field : SerializeField]
        public NpcData Npc2 { get; private set; }
    }
}