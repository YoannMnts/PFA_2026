using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using Naussilus.Gameplay.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class OptionMenuUI : MonoPhaseListener<OptionMenu>
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button muteButton;
        [SerializeField] private Button cancelButton;

        private bool isMuted;
        private OptionMenu current;

        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(OptionMenu phase)
        {
            if(current != null)
                current.Cancel();
            
            current = phase;
            group.Show();
            quitButton.onClick.AddListener(OnQuitButtonClicked);
            muteButton.onClick.AddListener(OnMuteButtonClicked);
            cancelButton.onClick.AddListener(OnCancelButtonClicked);
            base.OnPhaseBegin(phase);
        }


        protected override void OnPhaseEnd(OptionMenu phase)
        {
            if(current == null)
                return;
            
            group.Hide();
            quitButton.onClick.RemoveListener(OnQuitButtonClicked);
            muteButton.onClick.RemoveListener(OnMuteButtonClicked);
            cancelButton.onClick.RemoveListener(OnCancelButtonClicked);
            base.OnPhaseEnd(phase);
        }

        private void OnCancelButtonClicked()
        {
            if(current != null)
                current.SetResult(true);
        }
        
        private async void OnMuteButtonClicked()
        {
            isMuted = !isMuted;
            if (SoundDesignManager.instance != null)
            {
                SoundDesignManager.instance.CutMusic(isMuted);
            }

            if (ButtonFeedbacksManager.instance != null)
            {
                await ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(muteButton.gameObject);
            }
            
        }

        private async void OnQuitButtonClicked()
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                await ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(quitButton.gameObject);
            }
            Application.Quit();
        }
    }
}