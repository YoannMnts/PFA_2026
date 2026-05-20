using System;
using JetBrains.Annotations;
using Naussilus.Core;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class CategoryNpcSlot
    {
        public CategoryNpcSlot(Vector3 position, Npc npc = null)
        {
            SlotPosition = position;
            CurrentNpc = npc;
        }
        
        [CanBeNull] public Npc CurrentNpc { get; private set; }
        
        public Vector3 SlotPosition { get; private set; }

        protected internal bool TryAddNpc(Npc npc)
        {
            if (CurrentNpc != null)
                return false;
            
            CurrentNpc = npc;
            CurrentNpc?.SetNewPosition(SlotPosition);
            return true;
        }

        protected internal bool TryRemoveNpc(Npc npc)
        {
            if (CurrentNpc != npc)
                return false;
            
            CurrentNpc?.ReturnToLastPosition();
            CurrentNpc = null;
            return true;
        }

        protected internal void ClearNpc()
        {
            CurrentNpc = null;
        }
    }
}