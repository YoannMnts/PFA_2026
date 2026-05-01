
namespace Naussilus.Core
{
    public struct IntValue : IConditionEffectValue, INpcStat
    {
        public int Amount { get; private set; }

        public IntValue(IntValueData data)
        {
            Amount = data.Amount;
        }
    }
}
