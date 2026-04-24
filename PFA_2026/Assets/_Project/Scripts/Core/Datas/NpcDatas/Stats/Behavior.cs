namespace Naussilus.Core.NpcDatas
{
    public struct Behavior
    {
        public Behavior(BehaviorData data)
        {
            Amount = data.Amount;
            Name = data.Name;
            Data = data;
        }
        
        public int Amount { get; private set; }
        
        public string Name { get; private set; }
        
        public BehaviorData Data { get; private set; }
        public void SetNewAmount(int amount) => Amount = amount;
    }
}