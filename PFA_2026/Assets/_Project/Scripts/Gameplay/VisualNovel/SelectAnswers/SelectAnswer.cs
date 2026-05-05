using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Naussilus.Gameplay.VisualNovel
{
    public class SelectAnswer : PhaseCompletionSource<IAnswer>
    {
        public Dialogue CurrentDialogue {get; private set;}
        
        public IAnswer[] Answers => CurrentDialogue.Answers;
        
        public SelectAnswer(Dialogue dialogue)
        {
            CurrentDialogue = dialogue;
        }
    }
}