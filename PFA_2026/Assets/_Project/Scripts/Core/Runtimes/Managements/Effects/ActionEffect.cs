using System.Linq;
using JetBrains.Annotations;
using Naussilus.Core.Managements.ActionDatas;
using Naussilus.Core.Managers.Npcs;

namespace Naussilus.Core
{
    public class ActionEffect
    {
        public int CategoryIndex { get; private set; }

        [CanBeNull] public Npc Npc { get; private set; }
        
        public Category[] CurrentCategories { get; private set; }
        [CanBeNull] public ConditionalEffect[] Effects { get; private set; }

        public ActionEffect(ActionEffectData data, Category[] categories)
        {
            CategoryIndex = data.CategoryIndex - 1;
            Npc = NpcManager.TryGetNpc(data.NpcData?.GUID);
            CurrentCategories = categories;
            Effects = data.Effects?.Select(e => new ConditionalEffect(e)).ToArray();
        }
    }
}