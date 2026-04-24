using System;
using Naussilus.Core.Conditions;
using Naussilus.Core.Consequences;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    [Serializable]
    public class MentalStateValue : IConditionValue, IConsequenceValue
    {
        [field: SerializeField]
        public ScriptableObject Stat { get; private set; }
    }
}