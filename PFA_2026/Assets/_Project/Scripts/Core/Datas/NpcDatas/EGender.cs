using System;
using Naussilus.Core.Datas.Conditions;
using UnityEngine;

namespace Naussilus.Core.Datas.NpcDatas
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