using System;
using UnityEngine;

namespace Naussilus.Core.Managements.ActionDatas
{
    [Serializable]
    public class CategoryIndexData : INpcSelectorData
    {
        [field: SerializeField, Range(0, 10)]
        public int Index { get; private set; }
    }
}