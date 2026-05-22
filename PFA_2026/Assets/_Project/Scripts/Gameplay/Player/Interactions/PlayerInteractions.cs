using System.Collections.Generic;
using Helteix.ChanneledProperties.Priorities;
using Helteix.Singletons.SceneServices;
using Helteix.Tools.Phases;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Naussilus.Gameplay.Interactions
{
    public class PlayerInteractions : PlayerComponent, IPhaseListener<ManagementPhase>
    {
        private static readonly RaycastHit2D[] Hits = new RaycastHit2D[8];

        private Camera cam;
        

        [SerializeField] private TapInput tapInput;
        
        [SerializeField] private LayerMask interactionMask;
        public Priority<bool> CanInteract { get; private set; }

        private void Awake()
        {
            CanInteract = new Priority<bool>(true);
            cam = Camera.main;
            CanInteract.OnValueChanged += OnCanInteractChange;
        }

        private void OnDestroy()
        {
            CanInteract.OnValueChanged -= OnCanInteractChange;
        }

        private void OnEnable()
        {
            this.Register();
            if (gameObject.TryGetService(out PlayerController playerController))
                playerController.PlayerInputs.OnTouch += TryInteract;
        }

        private void OnDisable()
        {
            if (gameObject.TryGetService(out PlayerController playerController))
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
            Debug.Log($"CanInteract: {canInteract}");
            if (canInteract)
                Controller.PlayerInputs.AddTouchInput(tapInput);
            else
                Controller.PlayerInputs.RemoveTouchInput(tapInput);
        }

        private void TryInteract(ITouchInput touchInput)
        {
            Debug.Log($"Is Trigger Interact? {touchInput}");
            if (touchInput is not TapInput)
                return;

            EventSystem eventSystem = EventSystem.current;
            using (ListPool<RaycastResult>.Get(out var results))
            {
                var pointerEventData = new PointerEventData(eventSystem)
                {
                    position = tapInput.TapPosition
                };
                eventSystem.RaycastAll(pointerEventData, results);
        
                foreach (var result in results)
                {
                    if (result.gameObject.TryGetComponent(out IInteractable uiInteractable))
                    {
                        if (uiInteractable.IsInteractable())
                        {
                            uiInteractable.Interact(this);
                            return;
                        }
                    }
            
                    if (result.gameObject.GetComponent<RectTransform>() != null)
                        return;
                }
            }
        }

        public void StopInteract()
        {
            if (gameObject.TryGetService(out PlayerController playerInputManager))
            {
                playerInputManager.PlayerInputs.AddTouchInput(tapInput);
            }
        }
    }
}