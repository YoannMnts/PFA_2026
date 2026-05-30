using System;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using Naussilus.Core.Sounds;
using Naussilus.Gameplay.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay.PlayerSwitchs.UIs
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
            continueButton.onClick.AddListener(ContinueButtonClicked);
            if (SoundDesignManager.instance != null)
            {
                SoundDesignManager.instance.PlaySound(SoundsEnum.ChangePlayer);
            }
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(PlayerSwitch phase)
        {
            group.Hide();
            playerSwitch = null;
            continueButton.onClick.RemoveAllListeners();
            
            base.OnPhaseEnd(phase);
        }

        private async void ContinueButtonClicked()
        {
            try
            {
                if (ButtonFeedbacksManager.instance != null)
                {
                    ButtonFeedbacksManager.instance.ApplyButtonFeedbacks(continueButton.gameObject, 0.95f);
                    await Awaitable.WaitForSecondsAsync(0.7f);
                }
                playerSwitch.SetResult(true);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}