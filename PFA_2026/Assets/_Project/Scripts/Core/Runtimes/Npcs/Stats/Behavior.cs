using Naussilus.Core.NpcDatas;

namespace Naussilus.Core
{
    public struct Behavior : INpcStat, IConditionEffectValue, IConsequenceEffectValue
    {
        public Behavior(BehaviorValueData data)
        {
            Amount = data.Amount;
            Data = data.Stat;
        }
        
        public int Amount { get; private set; }
        
        public string Name => Data.Name;
        
        public BehaviorData Data { get; private set; }
        public void SetNewAmount(int amount) => Amount = amount;
    }
}