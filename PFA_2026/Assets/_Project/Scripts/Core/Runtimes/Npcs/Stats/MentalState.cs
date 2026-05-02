using Naussilus.Core.NpcDatas;

namespace Naussilus.Core
{
    public struct MentalState : INpcStat, IConditionEffectValue, IConsequenceEffectValue
    {
        public MentalState(MentalStateValueData data)
        {
            Amount = data.Amount;
            Data = data.Stat;
        }

        public int Amount { get; private set; }
        public string Name => Data.Name;
        
        public MentalStateData Data { get; private set; }

        public void SetNewAmount(int amount) => Amount = amount;
    }
}