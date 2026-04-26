using Naussilus.Core.Managers.Npcs;
using UnityEngine;

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
                Debug.Log($"TryGetNpc: GUID: {npcValue.NpcData.GUID}, Npc: {npc.Name}");
                Npc = npc;
            }
            else
            {
                Debug.Log($"Npc is not a NpcValue");
                Npc = null;
            }
        }
        
        public void SetNewAmount(int amount) => Amount = amount;
    
    }
}