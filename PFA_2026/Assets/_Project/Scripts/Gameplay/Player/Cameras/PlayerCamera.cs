using Helteix.ChanneledProperties.Priorities;
using Helteix.Singletons.SceneServices;
using Helteix.Tools.Phases;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class PlayerCamera : PlayerComponent, IPhaseListener<ManagementPhase>
    {
        public MonoCineCamera PlayerCam => playerCam;
        
        [SerializeField]
        private MonoCineCamera playerCam;
        
        [SerializeField]
        private Camera cam;
        
        [SerializeField]
        private Transform cameraTarget;
        
        [SerializeField]
        private PinchInput pinchInput;
        
        [SerializeField]
        private SlideInput slideInput;

        [SerializeField] 
        private float zoomSpeed = 0.01f;
        
        [SerializeField] 
        private float minZoom = 2f;
        
        [SerializeField] 
        private float maxZoom = 10f;

        [SerializeField]
        private float slideSpeed = 0.01f;
        
        public Priority<bool> CanMove { get; private set; }

        private void Awake()
        {
            CanMove = new Priority<bool>(true);
        }

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
            playerCam.CineCamera.Lens.OrthographicSize = Mathf.Clamp(
                cam.orthographicSize + pinchInput.Delta * zoomSpeed,
                minZoom,
                maxZoom
            );


            //Debug.Log($"Active cineCamera : {CineCameraManager.ActiveCamera} ");
            var transformPosition = VectorAddition(cameraTarget.position, (slideInput.Delta * slideSpeed));
            //Debug.Log($"Vector addition: {transformPosition}");
            cameraTarget.transform.position = transformPosition;
            playerCam.CineCamera.InternalUpdateCameraState(Vector3.up, Time.deltaTime);
        }

        private void CanMoveChange(bool canMove)
        {
            Debug.Log($"CanMoveChange {canMove}");
            if (canMove)
            {
                Controller.PlayerInputs.AddTouchInput(pinchInput);
                Controller.PlayerInputs.AddTouchInput(slideInput);
            }
            else
            {
                Controller.PlayerInputs.RemoveTouchInput(pinchInput);
                Controller.PlayerInputs.RemoveTouchInput(slideInput);
            }
        }
        
        private static Vector3 VectorAddition(Vector3 transformPosition, Vector2 slideInputDelta)
        {
            transformPosition.x -= slideInputDelta.x;
            transformPosition.y -= slideInputDelta.y;
            transformPosition.z = 0;
            return transformPosition;
        }

        public void OnPhaseBegin(ManagementPhase phase)
        {
            playerCam.SwitchToThisCamera();
            
            if (gameObject.TryGetService(out PlayerController playerController))
            {
                Debug.Log($"[PlayerCamera] Player camera on {phase}");
                playerController.PlayerInputs.AddTouchInput(pinchInput);
                playerController.PlayerInputs.AddTouchInput(slideInput);
            }
            CanMove.OnValueChanged += CanMoveChange;
        }

        public void OnPhaseEnd(ManagementPhase phase)
        {
            if (gameObject.TryGetService(out PlayerController playerController))
            {
                playerController.PlayerInputs.RemoveTouchInput(pinchInput);
                playerController.PlayerInputs.RemoveTouchInput(slideInput);
            }
            CanMove.OnValueChanged -= CanMoveChange;
        }

    }
}