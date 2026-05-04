using System;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class PlayerDefaultTouchInputs : MonoBehaviour
    {
        [SerializeReference] private ITouchInput[] touchInputs;
        private void OnEnable()
        {
            if (PlayerInputManager.TryGetFor(gameObject, out PlayerInputManager playerInputManager))
                for (int i = 0; i < touchInputs.Length; i++)
                    playerInputManager.AddTouchInput(touchInputs[i]);
        }

        private void OnDisable()
        {
            if (PlayerInputManager.TryGetFor(gameObject, out PlayerInputManager playerInputManager))
                for (int i = 0; i < touchInputs.Length; i++)
                    playerInputManager.RemoveTouchInput(touchInputs[i]);
        }
    }
}