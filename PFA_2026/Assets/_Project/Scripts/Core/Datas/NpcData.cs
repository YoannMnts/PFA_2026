using System;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [CreateAssetMenu(fileName = "NpcData", menuName = "NpcData", order = 0)]
    public class NpcData : ScriptableObject
    {
        [field : SerializeField]
        public string Name { get; private set; }
        
        [field : SerializeField]
        public Stats Stats { get; private set; }
        
        [field : SerializeField]
        public Gauge Gauge { get; private set; }
        
        [field : SerializeField]
        public Relationship[] Relationships { get; private set; }
        
        [field : SerializeField, TextArea]
        public string CurrentThinking { get; private set; }
    }
}