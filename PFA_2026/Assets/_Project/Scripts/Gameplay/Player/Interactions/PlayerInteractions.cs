using System;
using Helteix.ChanneledProperties.Priorities;
using Helteix.Singletons.SceneServices;
using Helteix.Tools.Phases;
using Rooms;
using UnityEngine;
using UnityEngine.LowLevelPhysics2D;

namespace Naussilus.Gameplay.Player.Interactions
{
    public class PlayerInteractions : PlayerComponent, IPhaseListener<ManagementPhase>
    {
        private static readonly RaycastHit2D[] Hits = new RaycastHit2D[8];
        
        [SerializeField]
        private TapInput tapInput;
        
        public Priority<bool> CanInteract { get; private set; }

        private void Awake()
        {
            CanInteract = new Priority<bool>(true);

            CanInteract.OnValueChanged += OnCanInteractChange;
        }

        private void OnEnable()
        {
            this.Register();
            if(gameObject.TryGetService(out PlayerController playerController))
                playerController.PlayerInputs.OnTouch += TryInteract;
        }

        private void OnDisable()
        {
            if(gameObject.TryGetService(out PlayerController playerController))
                playerController.PlayerInputs.OnTouch -= TryInteract;
            this.Unregister();
        }

        
        public void OnPhaseBegin(ManagementPhase phase)
        {
            if (gameObject.TryGetService(out PlayerController playerController))
            {
                playerController.PlayerInputs.AddTouchInput(tapInput);
            }
        }
        
        public void OnPhaseEnd(ManagementPhase phase)
        {
            if (gameObject.TryGetService(out PlayerController playerController))
            {
                playerController.PlayerInputs.RemoveTouchInput(tapInput);
            }
        }


        private void OnCanInteractChange(bool canInteract)
        {
            if (canInteract)
                Controller.PlayerInputs.AddTouchInput(tapInput);
            else
                Controller.PlayerInputs.RemoveTouchInput(tapInput);
        }
        
        public void TryInteract(ITouchInput touchInput)
        {
            Debug.Log($"AAAAAAAA");
            if (touchInput is not TapInput)
                return;

            Camera cam = Controller.PlayerCamera.Cam;
            Vector2 worldPos = cam.ScreenToWorldPoint(tapInput.TapPosition);
            
            //TODO mettre le vrai filter
            ContactFilter2D filter = ContactFilter2D.noFilter;
            int count = Physics2D.Raycast(worldPos, Vector2.zero,filter, Hits);
            IInteractable interactable = null;
            for (int i = 0; i < count; i++)
            {
                var hit = Hits[i];
                if (hit.transform.TryGetComponent(out IInteractable hitInteractable))
                {
                    if (interactable != null && interactable.Priority >= hitInteractable.Priority)
                        continue;
                    
                    if(hitInteractable.IsInteractable())
                        interactable = hitInteractable;
                }    
            }

            if (interactable != null)
                interactable.Interact(this);
            
        }

        public void StopInteract()
        { 
/*
            if (gameObject.TryGetService(out PlayerInputManager playerInputManager))
            {
                playerInputManager.AddTouchInput(tapInput);
            }
*/
        }
    }
}