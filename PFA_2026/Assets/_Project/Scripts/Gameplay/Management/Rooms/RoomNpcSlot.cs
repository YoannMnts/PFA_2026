using System;
using JetBrains.Annotations;
using Naussilus.Core;
using UnityEngine;

namespace Naussilus.Gameplay
{
    [Serializable]
    public class RoomNpcSlot
    {
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