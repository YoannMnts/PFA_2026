using UnityEngine;

namespace Naussilus.Core.Datas.Managements
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class RoomData : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public string Description { get; private set; }
        
        [field: SerializeField]
        public ActionData[] Actions { get; private set; }
    }
}