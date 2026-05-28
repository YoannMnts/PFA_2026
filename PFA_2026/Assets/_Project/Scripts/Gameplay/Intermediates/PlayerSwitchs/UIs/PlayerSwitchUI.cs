using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using Naussilus.Gameplay.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class PlayerSwitchUI : MonoPhaseListener<PlayerSwitch>
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private Button continueButton;

        private PlayerSwitch playerSwitch;
        
        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(PlayerSwitch phase)
        {
            group.Show();
            playerSwitch = phase;
            continueButton.onClick.AddListener(OnContinueButtonClicked);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(PlayerSwitch phase)
        {
            group.Hide();
            playerSwitch = null;
            continueButton.onClick.RemoveAllListeners();
            
            base.OnPhaseEnd(phase);
        }

        private void OnContinueButtonClicked()
        {
            ContinueButtonClicked();
        }

        private async void ContinueButtonClicked()
        {
            ButtonFeedbacksManager.instance.ApplyButtonFeedbacks(continueButton.gameObject, 0.95f);
            await Awaitable.WaitForSecondsAsync(0.7f);
            playerSwitch.SetResult(true);
        }
    }
}