using Helteix.ChanneledProperties.Priorities;
using Helteix.Singletons.SceneServices;
using Helteix.Tools.Phases;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

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


        void IPhaseListener<ManagementPhase>.OnPhaseBegin(ManagementPhase phase)
        {
            if (gameObject.TryGetService(out PlayerController playerController))
            {
                playerController.PlayerInputs.AddTouchInput(tapInput);
            }
        }

        void IPhaseListener<ManagementPhase>.OnPhaseEnd(ManagementPhase phase)
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
            //Debug.Log($"Is Trigger Interact? {touchInput}");
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

                for (var i = 0; i < results.Count; i++)
                {
                    var result = results[i];
                    Debug.Log($"Hit: {result.gameObject.name} at {result.screenPosition}");
                    if (result.gameObject.TryGetComponent(out IInteractable uiInteractable))
                    {
                        Debug.Log($"IInteractable: {uiInteractable}");
                        if (uiInteractable.IsInteractable())
                        {
                            uiInteractable.Interact(this);
                            Debug.Log($"Interact: {uiInteractable}");
                            return;
                        }
                    }

                    if (result.gameObject.TryGetComponent<RectTransform>(out var rect))
                        return;
                }
            }
        }
    }
}