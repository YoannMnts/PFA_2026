using System.Collections.Generic;
using System.Linq;
using Naussilus.Core.NpcDatas;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Naussilus.Core.Managements
{
    [CreateAssetMenu(fileName = "CategorySlot", menuName = "Naussilus/Management/CategorySlot", order = 0)]
    public class RoomSlotPositionData : ScriptableObject
    {
        public string SortingLayerName => sortingLayerName ?? "Default";
        
        [field: SerializeField]
        public Vector3 Position { get; private set; }
        
        [field: SerializeField]
        public ExpressionData Expression { get; private set; }

        [ValueDropdown("GetSortingLayers")]
        [SerializeField]
        private string sortingLayerName;

        [field: SerializeField]
        public int OrderInLayer { get; private set; }
        
        [field: SerializeField]
        public bool RandomFlip { get; private set; }
        
        [field: SerializeField]
        public bool Flip { get; private set; }
        
        [field: SerializeField, TextArea]
        public string Commentary { get; private set; }
        
        private IEnumerable<string> GetSortingLayers()
        {
            return SortingLayer.layers.Select(l => l.name);
        }
    }
}