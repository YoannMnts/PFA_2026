using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms
{
    public class CurrentlyInActionUI : MonoPhaseListener<CurrentlyInAction>
    {
        [SerializeField] private Button cancelButton;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private TMP_Text actionName;
        [SerializeField] private TMP_Text actionCountdown;

        private CurrentlyInAction currentlyInAction;
        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(CurrentlyInAction phase)
        {
            currentlyInAction = phase;
            group.Show();
            actionName.text = phase.CurrentAction.Name;
            actionCountdown.text = $"Jour restant : {phase.CurrentAction.Countdown}";
            cancelButton.onClick.AddListener(Cancel);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(CurrentlyInAction phase)
        {
            group.Hide();
            actionName.text = string.Empty;
            actionCountdown.text = string.Empty;
            currentlyInAction = null;
            cancelButton.onClick.RemoveAllListeners();
            
            base.OnPhaseEnd(phase);
        }

        private void Cancel()
        {
            currentlyInAction.SetResult(false);
        }
    }
}