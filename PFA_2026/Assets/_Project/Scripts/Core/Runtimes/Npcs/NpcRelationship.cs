using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;

namespace Naussilus.Core
{
    public class NpcRelationship : INpcStat, IConditionEffectValue, IConsequenceEffectValue
    {
        public int Amount { get; private set; }
        
        public Npc Npc { get; private set; }
        
        public NpcRelationshipData Data { get; private set; }
        
        public NpcRelationship(NpcRelationshipData data)
        {
            Data = data;
            Amount = data.Amount;
            
            switch (data.Npc)
            {
                case NpcValueData npcValue:
                    NpcManager.TryGetNpc(npcValue.NpcData?.GUID, out Npc npc);
                    Npc = npc;
                    break;
                default:
                    Npc = null;
                    break;
            }
        }
        
        public void SetNewAmount(int amount) => Amount = amount;
    }
}