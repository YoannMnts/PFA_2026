using System.Linq;
using JetBrains.Annotations;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core
{
    public struct Expression
    {
        [CanBeNull] public ExpressionValue[] Expressions { get; private set; }

        public Expression(ExpressionData data)
        {
            Expressions = data?.Expressions?.Select(e => new ExpressionValue(e)).ToArray();
        }
    }
}