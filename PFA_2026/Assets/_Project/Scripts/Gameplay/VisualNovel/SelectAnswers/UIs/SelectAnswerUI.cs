using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class SelectAnswerUI : MonoPhaseListener<SelectAnswer>
    {
        [SerializeField]
        private CanvasGroup group;
        [SerializeField]
        private AnswerUIList answerUIList;

        private SelectAnswer current;
        private void Start()
        {
            group.Hide();
        }

        protected override void OnPhaseBegin(SelectAnswer phase)
        {
            if(current != null)
                return;
            
            current = phase;
            group.Show();
            answerUIList.Connect(phase.Answers);
            
            base.OnPhaseBegin(phase);
        }

        protected override void OnPhaseEnd(SelectAnswer phase)
        {
            if(current != phase)
                return;
            
            group.Hide();
            answerUIList.Disconnect();
            current = null;
            
            base.OnPhaseEnd(phase);
        }

        public async Awaitable OnAnswerChoose(IAnswer answer)
        {
            await Awaitable.WaitForSecondsAsync(0.7f);
            current.SetResult(answer);
        }
    }
}