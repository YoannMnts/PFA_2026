using Helteix.ChanneledProperties.Priorities;
using Helteix.Singletons.SceneServices;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using Naussilus.Gameplay.Interactions;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class SwitchDayUI : MonoPhaseListener<SwitchDay>, IInteractable
    {
        public int Priority { get; private set; }
        
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        [SerializeField]
        private TMP_Text textArea;
        [SerializeField]
        private RectTransform boatImage;
        
        private SwitchDay currentSwitchDay;

        private void Start()
        {
            if (currentSwitchDay != null)
                return;
            canvasGroup.Hide();
        }

        protected override void OnPhaseBegin(SwitchDay phase)
        {
            if (currentSwitchDay != null)
                currentSwitchDay.Cancel();
            
            currentSwitchDay = phase;
            canvasGroup.Show();
            textArea.text = $"Jour {phase.CurrentDay}";

            if (gameObject.TryGetService(out PlayerController playerController))
            {
                playerController.PlayerInteractions.CanInteract.AddPriority(this, PriorityTags.Highest, true);
            }
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(SwitchDay phase)
        {
            if (currentSwitchDay == null)
                return;
            
            canvasGroup.Hide();
            textArea.text = string.Empty;
            currentSwitchDay = null;
            
            if (gameObject.TryGetService(out PlayerController playerController))
                playerController.PlayerInteractions.CanInteract.RemovePriority(this);
            
            base.OnPhaseEnd(phase);
        }
        
        public void Interact(PlayerInteractions playerInteractions)
        {
            Debug.Log($"Interacting with {currentSwitchDay}");
            currentSwitchDay.SetResult(true);
        }
    }
}