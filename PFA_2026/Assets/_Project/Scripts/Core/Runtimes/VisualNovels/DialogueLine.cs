using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.VisualNovels.EventDatas.DialogueDatas.DialogueLines;

namespace Naussilus.Core
{
    public struct DialogueLine
    {
        public Npc Npc { get; private set; }
        
        public Expression Expression { get; private set; }
        
        public string[] Text { get; private set; }

        public DialogueLine(DialogueLineData data)
        {
            Npc = NpcManager.TryGetNpc(data.Npc.GUID);
            Expression = new Expression(data.Expression);
            Text = data.Text;
        }
    }
}