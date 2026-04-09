using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay.VisualNovel._Project.Scripts
{
    public class DecisionButton : PhaseButton<DecisionChoice>
    {
        [SerializeField]
        private TextMeshProUGUI textMesh;
        
        private int index;

        protected override void OnButtonClicked()
        {
            currentPhase.SetResult(index);
            DisableInteraction();
        }

        public void InitButton(int i, string text)
        {
            index = i;
            textMesh.text = text;
        }
    }
}