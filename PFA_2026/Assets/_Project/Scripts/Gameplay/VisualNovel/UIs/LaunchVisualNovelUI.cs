using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
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