using Naussilus.Core.Managers.Npcs;
using UnityEngine;

namespace Naussilus.Core.NpcDatas
{
    public struct NpcRelationship : INpcStat
    {
        public int Amount { get; private set; }
        
        public Npc Npc { get; private set; }
        
        public NpcRelationshipData Data { get; private set; }
        
        public NpcRelationship(NpcRelationshipData data)
        {
            Data = data;
            Amount = data.Amount;
            
            if (data.Npc is NpcValue npcValue)
            {
                NpcManager.TryGetNpc(npcValue.NpcData.GUID, out Npc npc);
                Npc = npc;
            }
            else
            {
                Npc = null;
            }
        }
        
        public void SetNewAmount(int amount) => Amount = amount;
    
    }
}