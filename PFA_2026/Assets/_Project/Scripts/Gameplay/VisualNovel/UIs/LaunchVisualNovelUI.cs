using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using Naussilus.Gameplay.VisualNovel.VisualNovels;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel.UIs
{
    public class LaunchVisualNovelUI : MonoBehaviour, IPhaseListener<VisualNovelPhase>
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        private void Awake()
        {
            canvasGroup.Hide();
        }
        
        private void OnEnable()
        {
            this.Register();
        }

        private void OnDisable()
        {
            this.Unregister();
        }
        
        public void OnPhaseBegin(VisualNovelPhase phase)
        {
            canvasGroup.Show();
        }

        public void OnPhaseEnd(VisualNovelPhase phase)
        {
            canvasGroup.Hide();
        }
    }
}