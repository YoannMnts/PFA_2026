using System;
using System.Linq;
using Naussilus.Core.NpcDatas;
using Naussilus.Gameplay;
using UnityEngine;

namespace Naussilus.Core
{
    public class Npc : INpcSelector
    {
        public event Action<CategoryNpcSlot> OnAddedInSlot;
        public event Action OnRemoveSlot;
        
        public string Name { get; private set; }
        public Behavior[] Behaviors { get; private set; }
        public MentalState[] MentalStates { get; private set; }
        public EGender Gender { get; private set; }
        public NpcRelationship[] Relationships { get; private set; }
        public Sprite CategoryIcon { get; private set; }
        public string CurrentThinking { get; private set; }
        
        public Npc(NpcData npcData)
        {
            Name = npcData.Name;
            Behaviors = npcData.Behavior?.Select(b => new Behavior(b)).ToArray();
            MentalStates = npcData.MentalState?.Select(m => new MentalState(m)).ToArray();
            Gender = npcData.Gender;
            CategoryIcon = npcData.CategoryIcon;
            CurrentThinking = npcData.CurrentThinking;
        }

        public void InitRelationships(NpcData npcData)
        {
            Relationships = npcData.Relationships?.Select(r => new NpcRelationship(r)).ToArray();
        }

        public void AddedInSlot(CategoryNpcSlot slot)
        {
            OnAddedInSlot?.Invoke(slot);
        }

        public void RemoveSlot()
        {
            OnRemoveSlot?.Invoke();
        }
    }
}