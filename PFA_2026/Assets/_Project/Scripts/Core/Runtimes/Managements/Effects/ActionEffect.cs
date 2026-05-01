using System.Linq;
using Naussilus.Core.Managements.ActionDatas;
using Naussilus.Core.Managers.Npcs;
using UnityEngine;

namespace Naussilus.Core
{
    public struct ActionEffect
    {
        public int CategoryIndex { get; private set; }
        
        public Npc NpcData { get; private set; }
        
        public ConditionalEffect[] Effects { get; private set; }
        
        public Vector3 Position { get; private set; }
        
        public Expression ActionSprite { get; private set; }

        public ActionEffect(ActionEffectData data)
        {
            CategoryIndex = data.CategoryIndex;
            NpcData = NpcManager.TryGetNpc(data.NpcData.GUID);
            Effects = data.Effects.Select(e => new ConditionalEffect(e)).ToArray();
            Position = data.Position;
            ActionSprite = new Expression(data.ActionSprite);
        }
    }
}