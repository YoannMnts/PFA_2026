
using System;
using Naussilus.Core.Consequences;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class Gender : INpcSelector
    {
        [field: SerializeField]
        public EGender EGender { get; private set; }
    }
}