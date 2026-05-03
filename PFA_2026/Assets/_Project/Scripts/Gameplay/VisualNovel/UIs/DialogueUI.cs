using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel
{
    public class DialogueUI : MonoBehaviour, IPhaseListener<DialoguePhase>
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        [SerializeField]
        private TextMeshProUGUI textMesh;
        
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
        
        public void OnPhaseBegin(DialoguePhase phase)
        {
            //textMesh.text = phase.CurrentDialogue.Lines[0].Text[0]; //NEED TO MODIFY WITH LOOP
            canvasGroup.Show();
        }

        public void OnPhaseEnd(DialoguePhase phase)
        {
            canvasGroup.Hide();
        }
    }
}