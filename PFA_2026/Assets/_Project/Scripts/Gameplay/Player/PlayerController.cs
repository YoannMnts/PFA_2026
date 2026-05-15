using Helteix.ChanneledProperties.Priorities;
using Helteix.Singletons.SceneServices;
using Naussilus.Gameplay.Interactions;
using UnityEngine;

namespace Naussilus.Gameplay
{
    [RequireComponent(typeof(PlayerInputs))]
    [RequireComponent(typeof(PlayerInteractions))]
    [RequireComponent(typeof(PlayerCamera))]
    public class PlayerController : SceneService<PlayerController>
    {

        [field: SerializeField]
        public PlayerInputs PlayerInputs { get; private set; }
        [field: SerializeField]
        public PlayerCamera PlayerCamera { get; private set; }
        [field: SerializeField]
        public PlayerInteractions PlayerInteractions { get; private set; }

        private PlayerComponent[] playerComponents;
        
        private void Reset()
        {
            PlayerInputs = GetComponent<PlayerInputs>();
            PlayerCamera = GetComponent<PlayerCamera>();
            PlayerInteractions = GetComponent<PlayerInteractions>();
        }

        private void Awake()
        {
            playerComponents = GetComponentsInChildren<PlayerComponent>();

            for (int i = 0; i < playerComponents.Length; i++)
                playerComponents[i].Connect(this);
        }

        public void Freeze()
        {
            PlayerInteractions.CanInteract.AddPriority(this, PriorityTags.Highest, false);
        }

        public void Unfreeze()
        {
            PlayerInteractions.CanInteract.RemovePriority(this);
        }
    }
}