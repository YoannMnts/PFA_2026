using Helteix.Tools.Phases;
using Naussilus.Core.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class SwitchDayUI : MonoBehaviour, IPhaseListener<SwitchDay>
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        [SerializeField]
        private TextMeshProUGUI textArea;
        
        private SwitchDay currentSwitchDay;

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

        public void OnPhaseBegin(SwitchDay phase)
        {
            currentSwitchDay = phase;
            textArea.text = $"Jours {phase.CurrentDay}";
            canvasGroup.Show();
        }

        public void OnPhaseEnd(SwitchDay phase)
        {
            canvasGroup.Hide();
            currentSwitchDay = null;
        }
    }
}