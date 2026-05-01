using System.Linq;
using Naussilus.Core.NpcDatas;

namespace Naussilus.Core
{
    public struct Expression
    {
        public ExpressionValue[] Expressions { get; private set; }

        public Expression(ExpressionData data)
        {
            Expressions = data.Expressions?.Select(e => new ExpressionValue(e)).ToArray();
        }
    }
}