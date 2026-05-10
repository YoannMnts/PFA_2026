using Naussilus.Core.Consequences;
using Naussilus.Core.Operators;

namespace Naussilus.Core
{
    public class Consequence
    {
        public bool IsGameLost { get; private set; }
        
        public ConsequenceSide ConsequenceSide { get; private set; }
        
        public ArithmeticOperator ArithmeticOperator { get; private set; }
        
        public int Amount { get; private set; }
        
        public string[] Text { get; private set; }

        public Consequence(ConsequenceData data)
        {
            IsGameLost = data.IsGameLost;
            ConsequenceSide = new ConsequenceSide(data.ConsequenceSide);
            ArithmeticOperator = data.ArithmeticOperator;
            Amount = data.Amount;
            Text = data.Text;
        }
    }
}