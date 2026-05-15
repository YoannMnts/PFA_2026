using UnityEngine;

namespace Naussilus.Gameplay
{
    public class PlayerDefaultTouchInputs : MonoBehaviour
    {
        [SerializeReference] private ITouchInput[] touchInputs;
        private void OnEnable()
        {
            if (PlayerController.TryGetFor(gameObject, out PlayerController playerController))
                for (int i = 0; i < touchInputs.Length; i++)
                    playerController.PlayerInputs.AddTouchInput(touchInputs[i]);
        }

        private void OnDisable()
        {
            if (PlayerController.TryGetFor(gameObject, out PlayerController playerController))
                for (int i = 0; i < touchInputs.Length; i++)
                    playerController.PlayerInputs.RemoveTouchInput(touchInputs[i]);
        }
    }
}