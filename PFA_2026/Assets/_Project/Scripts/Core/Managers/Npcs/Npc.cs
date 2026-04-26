using System.Linq;
using Naussilus.Core.Consequences;
using Naussilus.Core.NpcDatas;
using UnityEngine.Pool;

namespace Naussilus.Core.Managers.Npcs
{
    public class Npc
    {
        public string Name { get; private set; }
        public Behavior[] Behaviors { get; private set; }
        public MentalState[] MentalStates { get; private set; }
        public EGender Gender { get; private set; }
        public NpcRelationship[] Relationships { get; private set; }
        public string CurrentThinking { get; private set; }
        
        public Npc(NpcData npcData)
        {
            Name = npcData.Name;
            Behaviors = npcData.Behavior.Select(b => new Behavior(b)).ToArray();
            MentalStates = npcData.MentalState.Select(m => new MentalState(m)).ToArray();
            Gender = npcData.Gender;
            CurrentThinking = npcData.CurrentThinking;
        }

        public void InitRelationships(NpcData npcData)
        {
            Relationships = npcData.Relationships.Select(r => new NpcRelationship(r)).ToArray();
        }
        
        private int GetRelationshipWith(Npc npc)
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

        private void SetRelationshipWith(Npc npc, int amount)
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
                
                case BehaviorValue value:
                    for (var i = 0; i < Behaviors.Length; i++)
                    {
                        var behavior = Behaviors[i];
                        if (behavior.Data == value.Stat)
                            return new[] { behavior.Amount };
                    }

                    break;
                
                case MentalStateValue value:
                    for (var i = 0; i < MentalStates.Length; i++)
                    {
                        var mentalState = MentalStates[i];
                        if (mentalState.Stat == value.Stat)
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
                case BehaviorValue value:
                    for (var i = 0; i < Behaviors.Length; i++)
                    {
                        var behavior = Behaviors[i];
                        if (behavior.Data == value.Stat)
                            behavior.SetNewAmount(amount);
                    }

                    break;
                
                case MentalStateValue value:
                    for (var i = 0; i < MentalStates.Length; i++)
                    {
                        var mentalState = MentalStates[i];
                        if (mentalState.Stat == value.Stat)
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