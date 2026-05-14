using System;
using System.Linq;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core
{
    public class Npc : INpcSelector
    {
        public event Action<Transform> OnSetNewPosition;
        public event Action OnReturnToLastPosition;
        
        public string Name { get; private set; }
        public Behavior[] Behaviors { get; private set; }
        public MentalState[] MentalStates { get; private set; }
        public EGender Gender { get; private set; }
        public NpcRelationship[] Relationships { get; private set; }
        public Sprite CategoryIcon { get; private set; }
        public string CurrentThinking { get; private set; }
        
        public Category CurrentCategory { get; private set; }
        
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

        public void SetCategory(Category category)
        {
            CurrentCategory = category;
        }

        public void SetNewPosition(Transform newPosition)
        {
            OnSetNewPosition?.Invoke(newPosition);
        }

        public void ReturnToLastPosition()
        {
            OnReturnToLastPosition?.Invoke();
        }
    }
}