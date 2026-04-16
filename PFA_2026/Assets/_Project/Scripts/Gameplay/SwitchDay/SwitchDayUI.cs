using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class SwitchDayUI : MonoBehaviour, IPhaseListener<SwitchDay>
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        [SerializeField]
        private TextMeshProUGUI textArea;
        
        private SwitchDay currentSwitchDay;

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

        public void OnPhaseBegin(SwitchDay phase)
        {
            currentSwitchDay = phase;
            textArea.text = $"Jours {phase.CurrentDay}";
            Debug.Log(textArea.text);
            canvasGroup.Show();
        }

        public void OnPhaseEnd(SwitchDay phase)
        {
            canvasGroup.Hide();
            currentSwitchDay = null;
        }
    }
}