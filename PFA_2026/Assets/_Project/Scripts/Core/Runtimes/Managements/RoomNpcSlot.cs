using System;
using JetBrains.Annotations;
using Naussilus.Core;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class RoomNpcSlot
    {
        public RoomNpcSlot(Vector3 position, Npc npc = null)
        {
            CurrentSlot = position;
            CurrentNpc = npc;
        }
        
        [CanBeNull] public Npc CurrentNpc { get; private set; }
        
        public Vector3 CurrentSlot { get; private set; }

        public bool TryAddNpc(Npc npc)
        {
            if (CurrentNpc != null)
                return false;
            
            CurrentNpc = npc;
            CurrentNpc?.SetNewPosition(CurrentSlot);
            return true;
        }

        public bool TryRemoveNpc(Npc npc)
        {
            if (CurrentNpc != npc)
                return false;
            
            CurrentNpc?.ReturnToLastPosition();
            CurrentNpc = null;
            return true;
        }
    }
}