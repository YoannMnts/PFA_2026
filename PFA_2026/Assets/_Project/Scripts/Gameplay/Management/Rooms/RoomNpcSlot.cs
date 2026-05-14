using System;
using JetBrains.Annotations;
using Naussilus.Core;
using UnityEngine;

namespace Rooms
{
    [Serializable]
    public class RoomNpcSlot
    {
        [field: SerializeField]
        [CanBeNull] public Npc CurrentNpc { get; private set; }
        
        [field: SerializeField]
        public Transform CurrentSlot { get; private set; }

        public bool TryAddNpc(Npc npc)
        {
            if (CurrentNpc != null)
                return false;
            
            CurrentNpc = npc;
            CurrentNpc?.SetNewPosition(CurrentSlot);
            return true;
        }

        public void TryRemoveNpc(Npc npc)
        {
            if (CurrentNpc != npc)
                return;
            
            CurrentNpc?.ReturnToLastPosition();
            CurrentNpc = null;
        }
    }
}