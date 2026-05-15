using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay.VisualNovel.SelectAnswers.UIs
{
    public class AnswerUI : UIItem<IAnswer>
    {
        [SerializeField]
        private TMP_Text textMesh;
        
        [SerializeField]
        private Button button;
        
        private SelectAnswerUI selectAnswer;

        private void Start()
        {
            selectAnswer = GetComponentInParent<SelectAnswerUI>();
        }
        protected override void SyncUI(IAnswer current)
        {
            textMesh.text = current.ButtonText;
            button.onClick.AddListener(OnButtonClicked);
        }

        protected override void ClearUI()
        {
            textMesh.text = string.Empty;
            selectAnswer = null;
            button.onClick.RemoveAllListeners();
        }

        private void OnButtonClicked()
        {
            selectAnswer.OnAnswerChoose(Current);
        }
    }
}