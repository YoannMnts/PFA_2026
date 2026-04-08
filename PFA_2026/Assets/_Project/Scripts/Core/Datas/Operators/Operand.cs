using System;
using Naussilus.Core.Datas.Conditions;
using Naussilus.Core.Datas.EStats;
using UnityEngine;

namespace Naussilus.Core.DatasOperators
{
    public interface IOperand
    {
        public ITarget Target { get; }
    }
    
    [Serializable]
    public struct IntOperand : IOperand
    {
        [field: SerializeField]
        public int Amount { get; private set; }

        [field: SerializeReference, SubclassSelector]
        public ITarget Target { get; private set; }
    }
}