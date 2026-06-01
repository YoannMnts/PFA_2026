using Helteix.Tools.Phases;
using Naussilus.Core;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class SelectAnswer : PhaseCompletionSource<IAnswer>
    {
        public IAnswer[] Answers {get; private set;}
        
        public SelectAnswer(IAnswer[] answer)
        {
            Answers = answer;
            //RandomizeAnswers(Answers);
        }

        private void RandomizeAnswers(IAnswer[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
            }
        }
    }
}