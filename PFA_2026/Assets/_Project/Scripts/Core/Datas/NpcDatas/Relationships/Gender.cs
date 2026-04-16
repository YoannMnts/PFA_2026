
using System;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class Gender : IRelationshipValue
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