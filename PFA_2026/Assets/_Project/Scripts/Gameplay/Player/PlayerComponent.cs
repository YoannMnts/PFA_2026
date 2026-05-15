using UnityEngine;

namespace Naussilus.Gameplay
{
    public abstract class PlayerComponent : MonoBehaviour
    {
        public PlayerController Controller { get; private set; }

        public void Connect(PlayerController controller)
        {
            Controller = controller;
        }
        
    }
}