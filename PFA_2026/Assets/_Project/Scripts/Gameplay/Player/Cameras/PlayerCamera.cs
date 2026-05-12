using System;
using Helteix.ChanneledProperties.Priorities;
using Helteix.Singletons.SceneServices;
using Helteix.Tools.Phases;
using Naussilus.Gameplay;
using Naussilus.Gameplay.Player;
using UnityEngine;

namespace Cameras
{
    public class PlayerCamera : PlayerComponent, IPhaseListener<ManagementPhase>
    {
        public Camera Cam => cam;
        
        [SerializeField]
        private Camera cam;
        
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
        
        public Priority<bool> CanPinchAndSlide { get; private set; }

        private void Awake()
        {
            CanPinchAndSlide = new Priority<bool>(true);
        }

        private void OnEnable()
        {
            CanPinchAndSlide.OnValueChanged += CanPinchAndSlideChange;
            this.Register();
        }


        private void OnDisable()
        {
            CanPinchAndSlide.OnValueChanged -= CanPinchAndSlideChange;
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
            
            cam.transform.position = VectorAddition(cam.transform.position, (slideInput.Delta * slideSpeed));
        }

        private void CanPinchAndSlideChange(bool canPinchAndSlide)
        {
            if (canPinchAndSlide)
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
            return transformPosition;
        }

        public void OnPhaseBegin(ManagementPhase phase)
        {
            if (gameObject.TryGetService(out PlayerController playerController))
            {
                playerController.PlayerInputs.AddTouchInput(pinchInput);
                playerController.PlayerInputs.AddTouchInput(slideInput);
            }
        }

        public void OnPhaseEnd(ManagementPhase phase)
        {
            if (gameObject.TryGetService(out PlayerController playerController))
            {
                playerController.PlayerInputs.RemoveTouchInput(pinchInput);
                playerController.PlayerInputs.RemoveTouchInput(slideInput);
            }
        }

    }
}