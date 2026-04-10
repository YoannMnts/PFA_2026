using System;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Operators
{
    // faire un dropdown pour choisir entre Npc, Category, Genre
    public interface IRelationshipOperand {}

    [Serializable]
    public struct CategoryOperand : IRelationshipOperand
    {
        [field: SerializeField]
        public int CategoryIndex { get; private set; }
    }
    
    [Serializable]
    public struct GenderOperand :IRelationshipOperand
    {
        [field: SerializeField]
        public EGender Gender { get; private set; }
    }
    
    [Serializable]
    public struct AllNpcOperand :IRelationshipOperand
    {
    }
}