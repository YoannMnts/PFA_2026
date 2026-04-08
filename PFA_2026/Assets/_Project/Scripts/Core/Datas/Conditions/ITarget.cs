using System;
using UnityEngine;

namespace Naussilus.Core.Datas.Conditions
{
    public interface ITarget {}

    [Serializable]
    public struct CategoryIndex : ITarget
    {
        [field: SerializeField]
        public int Index { get; private set; }
    }
}