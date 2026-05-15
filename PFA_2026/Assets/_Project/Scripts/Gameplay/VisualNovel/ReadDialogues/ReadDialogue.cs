using Helteix.Tools.Phases;
using Naussilus.Core;

namespace Naussilus.Gameplay.VisualNovel.ReadDialogues
{
    public class ReadDialogue : PhaseCompletionSource<bool>
    {
        public DialogueLine[] DialogueLines { get; private set; }
        
        public ReadDialogue(DialogueLine[] dialogue)
        {
            DialogueLines = dialogue;
        }
    }
}