using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers;

namespace Naussilus.Core
{
    public struct BasicAnswer : IAnswer
    {
        public Dialogue NextDialogue { get; private set; }

        public BasicAnswer(BasicAnswerData data)
        {
            NextDialogue = new Dialogue(data.NextDialogue);
        }
    }
}