using Helteix.Tools.Phases;
using Naussilus.Core.Managers;
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
        
        public void OnPhaseBegin(Dialogue phase)
        {
            //textMesh.text = phase.CurrentDialogue.Lines[0].Text[0]; //NEED TO MODIFY WITH LOOP
            canvasGroup.Show();
        }

        public void OnPhaseEnd(Dialogue phase)
        {
            canvasGroup.Hide();
        }
    }
}