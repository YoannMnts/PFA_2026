using Naussilus.Gameplay;
using UnityEngine;

namespace Naussilus.Core.Sounds
{
    [CreateAssetMenu(fileName = "SoundClipData", menuName = "Naussilus/Sound/SoundClipData")]
    public class SoundClipData : ScriptableObject
    {
        [field: SerializeField] 
        public SoundsEnum soundTag { get; private set; }
        
        [field: SerializeField] 
        public AudioClip clip { get; private set; }
    }
}
