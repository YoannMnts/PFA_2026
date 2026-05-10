using Naussilus.Core.Conditions;
using Naussilus.Core.Operators;

namespace Naussilus.Core
{
    public class Condition
    {
        public ConditionSide LeftSide { get; private set; }

        public ComparisonOperator ComparisonOperator { get; private set; }
        
        public ConditionSide RightSide { get; private set; }

        public Condition(ConditionData data)
        {
            LeftSide = new ConditionSide(data.LeftSide);
            ComparisonOperator = data.ComparisonOperator;
            RightSide = new ConditionSide(data.RightSide);
        }
    }
}