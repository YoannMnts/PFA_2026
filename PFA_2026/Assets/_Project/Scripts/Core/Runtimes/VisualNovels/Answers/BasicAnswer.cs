using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers;

namespace Naussilus.Core
{
    public struct BasicAnswer : IAnswer
    {
        public string ButtonText { get; private set; }
        public Dialogue NextDialogue { get; private set; }

        public BasicAnswer(BasicAnswerData data)
        {
            NextDialogue = new Dialogue(data.NextDialogue);
            ButtonText = data.ButtonText;
        }
    }
}