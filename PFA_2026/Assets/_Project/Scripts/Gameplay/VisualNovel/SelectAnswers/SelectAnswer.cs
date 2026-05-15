using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Naussilus.Gameplay.VisualNovel.SelectAnswers
{
    public class SelectAnswer : PhaseCompletionSource<IAnswer>
    {
        public IAnswer[] Answers {get; private set;}
        
        public SelectAnswer(IAnswer[] answer)
        {
            Answers = answer;
        }
    }
}