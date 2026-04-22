using System;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys
{
    [Serializable]
    public class CategoryIndex : IRelationshipValue, INpcSelector
    {
        [field: SerializeField, Range(0, 10)]
        public int Index { get; private set; }
    }
}