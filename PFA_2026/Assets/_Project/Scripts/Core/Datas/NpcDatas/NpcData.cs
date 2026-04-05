using UnityEngine;

namespace Naussilus.Core.Datas.NpcDatas
{
    [CreateAssetMenu(fileName = "NpcData", menuName = "NpcData", order = 0)]
    public class NpcData : ScriptableObject
    {
        [field : SerializeField]
        public string Name { get; private set; }
        
        [field : SerializeField]
        public NpcStats NpcStats { get; private set; }
        
        [field : SerializeField]
        public NpcGauge NpcGauge { get; private set; }
        
        [field : SerializeField]
        public NpcRelationship[] Relationships { get; private set; }
        
        [field : SerializeField, TextArea]
        public string CurrentThinking { get; private set; }
    }
}