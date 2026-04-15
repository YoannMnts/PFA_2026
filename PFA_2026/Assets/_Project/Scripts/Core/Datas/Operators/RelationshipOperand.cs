using System;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Operators
{
    public interface IRelationshipOperand {}

    [Serializable]
    public class CategoryOperand : IRelationshipOperand
    {
        [field: SerializeField]
        public int CategoryIndex { get; private set; }
    }
    
    [Serializable]
    public class GenderOperand :IRelationshipOperand
    {
        [field: SerializeField]
        public EGender Gender { get; private set; }
    }
    
    [Serializable]
    public class AllNpcOperand :IRelationshipOperand
    {
    }
}