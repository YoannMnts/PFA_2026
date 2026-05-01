using System.Linq;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.Answers;

namespace Naussilus.Core
{
    public struct FinalAnswer : IAnswer
    {
        public DialogueLine[] NpcText { get; private set; }
        
        public ConditionalEffect[] Effects { get; private set; }

        public FinalAnswer(FinalAnswerData data)
        {
            NpcText = data.NpcText.Select(t => new DialogueLine(t)).ToArray();
            Effects = data.Effects.Select(e => new ConditionalEffect(e)).ToArray();
        }
    }
}