using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [CreateAssetMenu(fileName = "ActionData", menuName = "ActionData", order = 0)]
    public class ActionData : ScriptableObject
    {
        [field: SerializeField]
        public string ActionName { get; private set; }
        
        [field: SerializeField]
        public int Cost { get; private set; }
        
        [field: SerializeField]
        public int Coontdown { get; private set; }
        
        [field: SerializeField, HideInInspector]
        public string Room { get; private set; }
        
        [field: SerializeField]
        public RoomCategory[] Categories { get; private set; }
        
        [field: SerializeField]
        public RoomAction[] Effects { get; private set; }
    }
}