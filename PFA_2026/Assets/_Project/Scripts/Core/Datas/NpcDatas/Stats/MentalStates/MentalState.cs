namespace Naussilus.Core.NpcDatas
{
    public struct MentalState
    {
        public MentalState(MentalStateValue data)
        {
            Amount = data.Amount;
            Name = data.Stat.name;
            Stat = data.Stat;
        }

        public int Amount { get; private set; }
        
        public string Name { get; private set; }
        
        public MentalStateData Stat { get; private set; }
        public void SetNewAmount(int amount) => Amount = amount;
    }
}