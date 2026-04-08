using System;
using Naussilus.Core.Datas.Conditions;
using Naussilus.Core.DatasOperators;
using UnityEngine;

namespace Naussilus.Core.Datas.EStats
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
        All,
    }
}