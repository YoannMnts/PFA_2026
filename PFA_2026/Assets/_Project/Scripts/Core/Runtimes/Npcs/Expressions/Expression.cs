using System.Linq;
using JetBrains.Annotations;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core
{
    public class Expression
    {
        [CanBeNull] public ExpressionValue[] Expressions { get; private set; }

        public Expression(ExpressionData data)
        {
            Expressions = data?.Expressions?.Select(e => new ExpressionValue(e)).ToArray();
        }

        public bool TryGetExpression(Npc npc, out Sprite sprite)
        {
            for (int i = 0; i < Expressions?.Length; i++)
            {
                if (Expressions[i].Npc != npc) 
                    continue;
                sprite = Expressions[i].Sprite;
                return true;
            }
            sprite = null;
            return false;
        }
    }
}