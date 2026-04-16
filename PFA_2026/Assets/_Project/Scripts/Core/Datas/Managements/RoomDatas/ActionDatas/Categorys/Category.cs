using System;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys
{
    [Serializable]
    public struct Category
    {
        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public int Quantity { get; private set; }
        
        [field: SerializeField]
        public NpcData[] ProhibitedNpc { get; private set; }
        
        [field: SerializeField]
        public NpcData[] ObligateNpc { get; private set; }
    }
    
    [Serializable]
    public class CategoryIndex : IRelationshipValue
    {
        [field: SerializeField, Range(1, 10)]
        public int Index { get; private set; }
    }
}