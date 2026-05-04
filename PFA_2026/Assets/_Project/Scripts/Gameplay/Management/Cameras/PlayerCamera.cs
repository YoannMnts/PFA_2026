using Helteix.Singletons.SceneServices;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class PlayerCamera : MonoBehaviour, IPhaseListener<ManagementPhase>
    {
        [SerializeField]
        private Camera cam;
        
        [SerializeField]
        private PinchInput pinchInput;
        
        [SerializeField]
        private SlideInput slideInput;
        
        [SerializeField]
        private TapInput tapInput;

        [SerializeField] 
        private float zoomSpeed = 0.01f;
        
        [SerializeField] 
        private float minZoom = 2f;
        
        [SerializeField] 
        private float maxZoom = 10f;

        [SerializeField]
        private float slideSpeed = 0.01f;

        private void OnEnable()
        {
            this.Register();
        }

        private void OnDisable()
        {
            this.Unregister();
        }

        private void Update()
        {
            //Debug.Log($"{slideInput.Delta}");
            cam.orthographicSize = Mathf.Clamp(
                cam.orthographicSize + pinchInput.Delta * zoomSpeed,
                minZoom,
                maxZoom
            );
            
            cam.transform.position *= slideInput.Delta * slideSpeed;
        }

        public void OnPhaseBegin(ManagementPhase phase)
        {
            if (gameObject.TryGetService(out PlayerInputManager playerInputManager))
            {
                playerInputManager.AddTouchInput(pinchInput);
                playerInputManager.AddTouchInput(slideInput);
                playerInputManager.AddTouchInput(tapInput);
            }
        }

        public void OnPhaseEnd(ManagementPhase phase)
        {
            if (gameObject.TryGetService(out PlayerInputManager playerInputManager))
            {
                playerInputManager.RemoveTouchInput(pinchInput);
                playerInputManager.RemoveTouchInput(slideInput);
                playerInputManager.RemoveTouchInput(tapInput);
            }
        }
    }
}