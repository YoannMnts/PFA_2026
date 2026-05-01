
using System;
using Naussilus.Core.Consequences;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class GenderData : INpcSelectorData
    {
        [field: SerializeField]
        public EGender EGender { get; private set; }
    }
}