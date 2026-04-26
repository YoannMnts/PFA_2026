using UnityEngine;
using UnityEngine.InputSystem;

namespace Naussilus.Gameplay
{ 
    public class PinchZoom : MonoBehaviour
    {
        [SerializeField] private float zoomSpeed = 0.01f;
        [SerializeField] private float minZoom = 2f;
        [SerializeField] private float maxZoom = 10f;

        private Camera cam;
        private float previousDistance;

        private void Awake() => cam = Camera.main;

        private void Update()
        {
            if (Touchscreen.current == null) return;
            if (Touchscreen.current.touches.Count != 2) return;

            var touch0 = Touchscreen.current.touches[0];
            var touch1 = Touchscreen.current.touches[1];

            float currentDistance = Vector2.Distance(
                touch0.position.ReadValue(),
                touch1.position.ReadValue()
            );

            previousDistance = Vector2.Distance(
                touch0.position.ReadValue() - touch0.delta.ReadValue(),
                touch1.position.ReadValue() - touch1.delta.ReadValue()
            );

            float delta = previousDistance - currentDistance;

            cam.orthographicSize = Mathf.Clamp(
                cam.orthographicSize + delta * zoomSpeed,
                minZoom,
                maxZoom
            );
        }
    }
}