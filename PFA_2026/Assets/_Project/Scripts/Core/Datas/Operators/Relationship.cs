using System;
using UnityEngine;

namespace Naussilus.Core.Operators
{
    [Serializable]
    public struct Relationship
    {
        [field: SerializeField]
        public ERelationship ERelationship { get; private set; }
    }

    public enum ERelationship
    {
        Jennie,
        Tea,
        Marco,
        Camil,
        Herlock,
        Ynna,
        Liski,
        Pam,
        AllNpc,
        Category1,
        Category2,
        Category3,
    }
}