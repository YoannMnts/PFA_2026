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
            var allText = string.Empty;
            for (int i = 0; i < phase.CurrentDialogue.Lines.Length; i++)
            {
                for (int j = 0; j < phase.CurrentDialogue.Lines[i].Text.Length; j++)
                {
                    allText += phase.CurrentDialogue.Lines[i].Text[j];
                }
            }
            textMesh.text = allText; //NEED TO MODIFY WITH LOOP
            canvasGroup.Show();
        }

        public void OnPhaseEnd(DialoguePhase phase)
        {
            canvasGroup.Hide();
        }
    }
}