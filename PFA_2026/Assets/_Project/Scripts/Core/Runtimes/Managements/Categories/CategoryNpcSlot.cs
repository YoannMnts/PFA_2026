using JetBrains.Annotations;
using Naussilus.Core;
using Naussilus.Core.Managements;
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
            CurrentNpc = npc;
        }
        
        [CanBeNull] public Npc CurrentNpc { get; private set; }
        
        public Vector3 SlotPosition { get; private set; }
        
        public Expression NpcExpression { get; private set; }
        
        public string SpriteSortingLayerName { get; private set; }
        
        public int OrderInLayer { get; private set; }
        
        public bool Flip { get; private set; }

        protected internal bool TryAddNpc(Npc npc)
        {
            if (CurrentNpc != null)
                return false;
            
            CurrentNpc = npc;
            CurrentNpc?.AddedInSlot(this);
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
            CurrentNpc = null;
        }
    }
}