using Helteix.Tools.Phases;
using Naussilus.Core.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class DialogueUI : MonoBehaviour, IPhaseListener<Dialogue>
    {
        [SerializeField]
        private CanvasGroup canvasGroup;
        
        [SerializeField]
        private TextMeshProUGUI textMesh;
        
        public void OnPhaseBegin(Dialogue phase)
        {
            textMesh.text = phase.CurrentDialogue.Lines.Text;
            canvasGroup.Show();
        }

        public void OnPhaseEnd(Dialogue phase)
        {
            canvasGroup.Hide();
        }
    }
}