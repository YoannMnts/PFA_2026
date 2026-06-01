using Helteix.Tools.Phases.Listeners;
using Naussilus.Core.Managers;
using Naussilus.Gameplay.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class EndSceneMenu : MonoPhaseListener<EndingPhase>
    {
        [SerializeField]
        private GameObject goodEndingSprite;
        
        [SerializeField]
        private GameObject badEndingSprite;

        [SerializeField] private TextMeshProUGUI loseText;
        
        protected override void OnPhaseBegin(EndingPhase phase)
        {
            if (phase.IsGameOver)
            {
                badEndingSprite.SetActive(true);
                goodEndingSprite.SetActive(false);
            }
            else
            {
                goodEndingSprite.SetActive(true);
                badEndingSprite.SetActive(false);
            }
            if (SoundDesignManager.instance != null)
            {
                SoundDesignManager.instance.ManagmentMusic(true);
            }
            loseText.text = ConditionalEffectManager.lostConsequence.Text[0];
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(EndingPhase phase)
        {
            base.OnPhaseEnd(phase);
        }

        public async void ReturnToMainMenu(GameObject button)
        {
            if (ButtonFeedbacksManager.instance != null)
            {
                await ButtonFeedbacksManager.instance.ApplyButtonsFeedbacks(button);
            }
            SceneManager.LoadScene(0);
        }
    }
}