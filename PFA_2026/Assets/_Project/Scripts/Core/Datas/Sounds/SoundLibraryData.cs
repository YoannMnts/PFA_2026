using System.Collections.Generic;
using UnityEngine;

namespace Naussilus.Core.Sounds
{
    [CreateAssetMenu(fileName = "SoundLibraryData", menuName = "Naussilus/Sound/SoundLibraryData")]
    public class SoundLibraryData : ScriptableObject
    {
        [field: SerializeField] 
        public AudioClip visualNovelMusic { get; private set; }
        
        [field: SerializeField] 
        public AudioClip managmentMusic { get; private set; }
        
        [field: SerializeField] 
        public AudioClip menuMusic { get; private set; }
        
        [field: SerializeField] 
        public List<SoundClipData> clipsList { get; private set; }
    }
}
