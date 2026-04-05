using Naussilus.Core.Scripts.Categorys;
using Naussilus.Core.Scripts.Effects;
using UnityEngine;

namespace Naussilus.Core.Scripts
{
    [CreateAssetMenu(fileName = "ActionData", menuName = "ActionData", order = 0)]
    public class ActionData : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public int Cost { get; private set; }
        
        [field: SerializeField]
        public Category[] Categories { get; private set; }
        
        [field: SerializeField]
        public Effect Effects { get; private set; }
    }
}