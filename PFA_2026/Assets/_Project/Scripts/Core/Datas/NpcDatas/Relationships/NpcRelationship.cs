using Naussilus.Core.Managers.Npcs;

namespace Naussilus.Core.NpcDatas
{
    public struct NpcRelationship
    {
        public int Amount { get; private set; }
        
        public Npc Npc { get; private set; }
        
        public NpcRelationship(NpcRelationshipData data)
        {
            Amount = data.Amount;
            
            if (data.Npc is NpcValue npcValue)
            {
                NpcManager.TryGetNpc(npcValue.NpcData.GUID, out Npc npc);
                Npc = npc;
            }
            Npc = null;
        }
        
        public void SetNewAmount(int amount) => Amount = amount;
    
    }
}