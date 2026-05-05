using System;
using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay.VisualNovel
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
            selectAnswer = GetComponentInChildren<SelectAnswerUI>();
        }
        protected override void SyncUI(IAnswer current)
        {
            textMesh.text = current.ButtonText;
        }

        protected override void ClearUI()
        {
            textMesh.text = string.Empty;
            selectAnswer = null;
        }
        
        public void OnButtonClicked()
        {
            selectAnswer.OnAnswerChoose(Current);
        }
    }
}