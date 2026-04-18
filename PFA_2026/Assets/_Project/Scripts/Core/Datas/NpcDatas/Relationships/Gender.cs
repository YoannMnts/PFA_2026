
using System;
using Naussilus.Core.Consequences;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class Gender : IRelationshipValue, INpcSelector
    {
        [field: SerializeField]
        public EGender EGender { get; private set; }
    }
    
    public enum EGender
    {
        NonBinary,
        Woman,
        Man,
    }
}