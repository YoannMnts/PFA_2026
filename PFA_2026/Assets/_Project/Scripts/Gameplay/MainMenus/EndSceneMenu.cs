using Helteix.Tools.Phases.Listeners;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Naussilus.Gameplay
{
    public class EndSceneMenu : MonoPhaseListener<EndingPhase>
    {
        [SerializeField]
        private Image background;
        
        [SerializeField]
        private Sprite goodEndingSprite;
        
        [SerializeField]
        private Sprite badEndingSprite;
        
        protected override void OnPhaseBegin(EndingPhase phase)
        {
            background.sprite = phase.IsGameOver ? badEndingSprite : goodEndingSprite;
            if (SoundDesignManager.instance != null)
            {
                SoundDesignManager.instance.ManagmentMusic(true);
            }
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(EndingPhase phase)
        {
            background.sprite = null;
            base.OnPhaseEnd(phase);
        }

        public void OnClick()
        {
            SceneManager.LoadScene(0);
        }
    }
}