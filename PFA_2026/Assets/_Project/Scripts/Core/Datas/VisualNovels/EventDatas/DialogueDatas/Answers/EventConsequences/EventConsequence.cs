using System;
using Naussilus.Core.Datas.Conditions;
using Naussilus.Core.DatasOperators;
using UnityEngine;

namespace Naussilus.Core.Datas.VisualNovels
{
    [Serializable]
    public struct EventConsequence
    {
        [field : SerializeReference]
        public IOperand LeftOperand { get; private set; }
        
        [field : SerializeField]
        public EMathOperator Operator { get; private set; }
        
        [field : SerializeField]
        public int RightOperand { get; private set; }
        
        [field : SerializeReference, SubclassSelector]
        public ITarget[] Target { get; private set; }
    }
}