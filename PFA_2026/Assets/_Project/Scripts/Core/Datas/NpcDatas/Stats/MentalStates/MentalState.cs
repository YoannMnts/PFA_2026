namespace Naussilus.Core.NpcDatas
{
    public struct MentalState
    {
        public string Name => Data.Name;
        
        
        public MentalState(MentalStateValue data)
        {
            Amount = data.Amount;
            Data = data.Stat;
        }

        public int Amount { get; private set; }
        public MentalStateData Data { get; private set; }
        
        
        public void SetNewAmount(int amount) => Amount = amount;
    }
}