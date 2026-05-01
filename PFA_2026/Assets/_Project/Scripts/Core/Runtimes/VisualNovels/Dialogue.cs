using System.Linq;
using Naussilus.Core.Managers;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas;

namespace Naussilus.Core
{
    public struct Dialogue
    {
        public DialogueLine[] Lines { get; private set; }
        
        public IAnswer[] Answers { get; private set; }

        public Dialogue(DialogueData data)
        {
            Lines = data.Lines?.Select(l => new DialogueLine(l)).ToArray();
            Answers = data.Answers?.Select(a => a?.GetAnswer()).ToArray();
        }
    }
}