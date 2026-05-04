using System;
using Helteix.Singletons.SceneServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace Naussilus.Gameplay
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField]
        private Camera cam;
        
        [SerializeField]
        private PinchInput pinchInput;

        [SerializeField] private float zoomSpeed = 0.01f;
        [SerializeField] private float minZoom = 2f;
        [SerializeField] private float maxZoom = 10f;

        private void OnEnable()
        {
            if(gameObject.TryGetService(out PlayerInputManager playerInputManager))
                playerInputManager.AddTouchInput(pinchInput);
        }

        private void OnDisable()
        {
            if(gameObject.TryGetService(out PlayerInputManager playerInputManager))
                playerInputManager.RemoveTouchInput(pinchInput);
        }

        private void Update()
        {
            cam.orthographicSize = Mathf.Clamp(
                cam.orthographicSize + pinchInput.Delta * zoomSpeed,
                minZoom,
                maxZoom
            );
        }
    }
}