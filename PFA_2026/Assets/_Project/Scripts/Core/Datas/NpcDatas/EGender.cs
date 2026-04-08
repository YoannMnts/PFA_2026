using System;
using Naussilus.Core.Conditions;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public struct Gender : ITarget
    {
        [field : SerializeField]
        public EGender EGender { get; private set; }
    }
    
    public enum EGender
    {
        NonBinary,
        Woman,
        Man,
    }
}