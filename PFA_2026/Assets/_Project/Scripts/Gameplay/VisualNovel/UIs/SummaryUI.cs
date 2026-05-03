using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel
{
    public class SummaryUI : MonoBehaviour, IPhaseListener<Summary>
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        private void Start()
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
        
        public void OnPhaseBegin(Summary phase)
        {
            canvasGroup.Show();
        }

        public void OnPhaseEnd(Summary phase)
        {
            canvasGroup.Hide();
        }
    }
}