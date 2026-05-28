using System.Linq;
using JetBrains.Annotations;
using Naussilus.Core;
using Naussilus.Core.Managements;
using Naussilus.Core.Managers;
using Naussilus.Core.Managers.Npcs;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class CategoryNpcSlot
    {
        public CategoryNpcSlot(RoomSlotPositionData data, Npc npc = null)
        {
            SlotPosition = data == null ? Vector3.zero : data.Position;
            NpcExpression = new Expression(data.Expression); 
            SpriteSortingLayerName = data.SortingLayerName;
            OrderInLayer = data.OrderInLayer;
            Flip = data.RandomFlip ? Random.value > .5f : data.Flip;
            DefaultNpcs = data.IsDefaultSlotFor?.Select(npcData => NpcManager.TryGetNpc(npcData?.GUID)).ToArray();
            CurrentNpc = npc;
            Data = data;
            
            if (DefaultNpcs?.Length > 0)
            {
                this.Register();
            }
        }

        CategoryNpcSlot()
        {
            
        }
        
        [CanBeNull] public Npc CurrentNpc { get; private set; }
        
        public Vector3 SlotPosition { get; private set; }
        
        [CanBeNull] public Expression NpcExpression { get; private set; }
        
        public string SpriteSortingLayerName { get; private set; }
        
        public int OrderInLayer { get; private set; }
        
        public bool Flip { get; private set; }
        
        [CanBeNull] public Npc[] DefaultNpcs { get; private set; }
        
        public RoomSlotPositionData Data { get; private set; }

        protected internal bool TryAddNpc(Npc npc, Category category)
        {
            if (CurrentNpc != null)
                return false;
                
            CurrentNpc = npc;
            CurrentNpc?.AddedInSlot(this, category);
            return true;
        }

        protected internal bool TryRemoveNpc(Npc npc)
        {
            if (CurrentNpc != npc)
                return false;
            
            CurrentNpc?.RemoveSlot();
            CurrentNpc = null;
            return true;
        }

        protected internal void ClearNpc()
        {
            CurrentNpc?.RemoveSlot();
            CurrentNpc = null;
        }

        public CategoryNpcSlot Clone(Npc npc = null) =>
            new()
            {
                SlotPosition = SlotPosition,
                NpcExpression = NpcExpression, 
                SpriteSortingLayerName = SpriteSortingLayerName,
                OrderInLayer = OrderInLayer,
                Flip = Flip,
                CurrentNpc = npc
            };
    }
}