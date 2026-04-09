using System;
using UnityEngine;

namespace Naussilus.Core.Operators
{
    // faire un dropdown pour choisir entre Npc, Category, Genre
    [Serializable]
    public struct Relationship
    {
        [field: SerializeField]
        public ERelationship ERelationship { get; private set; }
    }

    [Flags]
    public enum ERelationship
    {
        Jennie = 1 << 0,
        Tea = 1 << 1,
        Marco = 1 << 2,
        Camil = 1 << 3,
        Herlock = 1 << 4,
        Ynna = 1 << 5,
        Liski = 1 << 6,
        Pam = 1 << 7,
        Category1,
        Category2,
        Category3,
        Males = Marco | Herlock | Liski,
        Females = Pam | Camil | Ynna,
    }

    public static class ERelationshipExtensions
    {
        public static bool HasFlagFast(this ERelationship value, ERelationship flag)
        {
            return (value & flag) != 0;
        }
    }
}