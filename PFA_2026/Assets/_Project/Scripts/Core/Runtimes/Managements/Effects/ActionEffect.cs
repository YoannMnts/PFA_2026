using System.Linq;
using JetBrains.Annotations;
using Naussilus.Core.Managements.ActionDatas;
using Naussilus.Core.Managers.Npcs;
using UnityEngine;

namespace Naussilus.Core
{
    public class ActionEffect
    {
        public int CategoryIndex { get; private set; }

        [CanBeNull] public Npc Npc { get; private set; }
        
        public Category[] CurrentCategories { get; private set; }
        [CanBeNull] public ConditionalEffect[] Effects { get; private set; }
        
        public Vector3 Position { get; private set; }
        
        public Expression? ActionSprite { get; private set; }

        public ActionEffect(ActionEffectData data, Category[] categories)
        {
            CategoryIndex = data.CategoryIndex;
            Npc = NpcManager.TryGetNpc(data.NpcData?.GUID);
            CurrentCategories = categories;
            Effects = data.Effects?.Select(e => new ConditionalEffect(e)).ToArray();
            Position = data.Position;
            ActionSprite = data.ActionSprite != null ? new Expression(data.ActionSprite) : null;
        }
    }
}