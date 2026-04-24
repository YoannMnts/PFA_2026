namespace Naussilus.Core.NpcDatas
{
    public struct MentalState
    {
        public MentalState(MentalStateData data)
        {
            Amount = data.Amount;
            Name = data.Name;
            Data = data;
        }

        public int Amount { get; private set; }
        
        public string Name { get; private set; }
        
        public MentalStateData Data { get; private set; }
        public void SetNewAmount(int amount) => Amount = amount;
    }
}