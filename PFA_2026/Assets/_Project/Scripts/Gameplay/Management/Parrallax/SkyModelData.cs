using UnityEngine;

namespace Naussilus.Gameplay.Parrallax
{
    [CreateAssetMenu(fileName = "SkyModelData", menuName = "Naussilus/Management/SkyModelData")]
    public class SkyModelData : ScriptableObject
    {
        public Sprite background;
        public Sprite[] clouds;
    }
}
