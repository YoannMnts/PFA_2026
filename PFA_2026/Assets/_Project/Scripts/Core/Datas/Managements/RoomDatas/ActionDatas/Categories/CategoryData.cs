using System;
using System.Buffers;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core.Managements.ActionDatas
{
    [Serializable]
    public class CategoryData
    {
        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public int Quantity { get; private set; }
        
        [field: SerializeField]
        public NpcData[] ProhibitedNpc { get; private set; }
        
        [field: SerializeField]
        public NpcData[] ObligateNpc { get; private set; }
        
        [field: SerializeField]
        public RoomSlotPositionData[] SlotPositions { get; private set; }

        protected internal void MakeSlotPositions()
        {
            if (SlotPositions != null && SlotPositions.Length == Quantity)
                return;
            
            var slots = new RoomSlotPositionData[Quantity];
            for (int i = 0; i < SlotPositions?.Length; i++)
            {
                if (i > slots.Length - 1)
                    break;
                
                slots[i] = SlotPositions[i];
            }
            SlotPositions = slots;
        }
    }
}