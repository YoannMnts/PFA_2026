using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using UnityEngine;

namespace Naussilus.Core
{
    public struct ExpressionValue
    {
        public Sprite Sprite { get; private set; }
        
        public Npc Npc { get; private set; }

        public ExpressionValue(ExpressionValueData data)
        {
            Sprite = data.Sprite;
            Npc = NpcManager.TryGetNpc(data.Npc?.GUID);
        }
    }
}