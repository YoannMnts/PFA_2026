
namespace Naussilus.Core
{
    public struct InValue : IConditionEffectValue, INpcStat
    {
        public int Amount { get; private set; }

        public InValue(IntValueData data)
        {
            Amount = data.Amount;
        }
    }
}
