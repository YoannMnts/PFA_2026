using System;

namespace Naussilus.Core.Managers.Npcs
{
    public interface INpcClickListener : IComparable<INpcClickListener>
    {
        int NpcClickPriority { get; }
        void OnNpcClick(Npc npc);
        
        int IComparable<INpcClickListener>.CompareTo(INpcClickListener other)
        {
            return other.NpcClickPriority.CompareTo(NpcClickPriority);
        }
    }
}