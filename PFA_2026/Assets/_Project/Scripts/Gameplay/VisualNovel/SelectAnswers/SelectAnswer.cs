using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Naussilus.Gameplay
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