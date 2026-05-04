using Helteix.Singletons.SceneServices;
using Helteix.Tools.Phases;
using Rooms;
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
            tapInput.OnTap += TryInteract;
        }

        private void OnDisable()
        {
            this.Unregister();
            tapInput.OnTap -= TryInteract;
        }

        private void Update()
        {
            //Debug.Log($"{slideInput.Delta}");
            cam.orthographicSize = Mathf.Clamp(
                cam.orthographicSize + pinchInput.Delta * zoomSpeed,
                minZoom,
                maxZoom
            );
            
            cam.transform.position = VectorAddition(cam.transform.position, (slideInput.Delta * slideSpeed));
        }

        private static Vector3 VectorAddition(Vector3 transformPosition, Vector2 slideInputDelta)
        {
            transformPosition.x -= slideInputDelta.x;
            transformPosition.y -= slideInputDelta.y;
            return transformPosition;
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

        public void TryInteract(Vector2 screenPos)
        {
            Vector2 worldPos = cam.ScreenToWorldPoint(screenPos);
    
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.transform.TryGetComponent(out MonoRoom monoRoom))
            {
                monoRoom.OnClick();
            }
        }
    }
}