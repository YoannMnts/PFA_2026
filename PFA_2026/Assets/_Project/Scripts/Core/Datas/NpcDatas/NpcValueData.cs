using System;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class NpcValueData : INpcSelectorData
    {
        [field: SerializeField]
        public NpcData NpcData { get; private set; }
    }
}