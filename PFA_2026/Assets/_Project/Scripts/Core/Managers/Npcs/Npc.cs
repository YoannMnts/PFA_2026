using System.Linq;
using Naussilus.Core.Consequences;
using Naussilus.Core.NpcDatas;
using UnityEngine.Pool;

namespace Naussilus.Core.Managers.Npcs
{
    public class Npc
    {
        public string Name { get; private set; }
        public NpcBehavior[] Behaviors { get; private set; }
        public NpcMentalState[] MentalStates { get; private set; }
        public EGender Gender { get; private set; }
        public NpcRelationship[] Relationships { get; private set; }
        public string CurrentThinking { get; private set; }
        
        public Npc(NpcData npcData)
        {
            Name = npcData.Name;
            Behaviors = npcData.Behavior.Select(b => b.Clone()).ToArray();
            MentalStates = npcData.MentalState.Select(m => m.Clone()).ToArray();
            Gender = npcData.Gender;
            Relationships = npcData.Relationships.Select(r => new NpcRelationship(r)).ToArray();
            CurrentThinking = npcData.CurrentThinking;
        }

        public int GetRelationshipWith(Npc npc)
        {
            for (int i = 0; i < Relationships.Length; i++)
            {
                if (Relationships[i].Npc == npc)
                {
                    return Relationships[i].Amount;
                }
            }
            return -1;
        }
        
        public void SetRelationshipWith(Npc npc, int amount)
        {
            for (int i = 0; i < Relationships.Length; i++)
            {
                if (Relationships[i].Npc == npc)
                {
                    Relationships[i].SetNewAmount(amount);;
                }
            }
        }
        
        public int[] GetValue(IConditionalEffect conditionalEffect)
        {
            switch (conditionalEffect)
            {
                case IntValue intValue:
                    return new []{intValue.Amount};
                
                case NpcBehavior value:
                    for (var i = 0; i < Behaviors.Length; i++)
                    {
                        var behavior = Behaviors[i];
                        if (behavior.Behavior == value.Behavior)
                            return new[] { behavior.Amount };
                    }

                    break;
                
                case NpcMentalState value:
                    for (var i = 0; i < MentalStates.Length; i++)
                    {
                        var mentalState = MentalStates[i];
                        if (mentalState.MentalState == value.MentalState)
                            return new[] { mentalState.Amount };
                    }

                    break;
                
                case NpcRelationshipData value:
                    using (ListPool<int>.Get(out var list))
                    {
                        NpcManager.GetSelectedNpcs(value.Npc, this, out Npc[] selectedNpcs);
                        for (int i = 0; i < selectedNpcs.Length; i++)
                        {
                            list.Add(GetRelationshipWith(selectedNpcs[i]));   
                        }
                        return list.ToArray();
                    }
            }
            return null;
        }
        
        public void SetValue(IConsequenceValue consequenceValue, int amount)
        {
            switch (consequenceValue)
            {
                case NpcBehavior value:
                    for (var i = 0; i < Behaviors.Length; i++)
                    {
                        var behavior = Behaviors[i];
                        if (behavior.Behavior == value.Behavior)
                            behavior.SetNewAmount(amount);
                    }

                    break;
                
                case NpcMentalState value:
                    for (var i = 0; i < MentalStates.Length; i++)
                    {
                        var mentalState = MentalStates[i];
                        if (mentalState.MentalState == value.MentalState)
                            mentalState.SetNewAmount(amount);
                    }

                    break;
                
                case NpcRelationshipData value:
                    NpcManager.GetSelectedNpcs(value.Npc, this, out Npc[] selectedNpcs);
                    for (int i = 0; i < selectedNpcs.Length; i++)
                        SetRelationshipWith(selectedNpcs[i], amount);   
                    break;
            }
        }
    }
}