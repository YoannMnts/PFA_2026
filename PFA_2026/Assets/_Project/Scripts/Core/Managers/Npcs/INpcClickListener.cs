using System;
using Naussilus.Core;


namespace Naussilus.Gameplay.Management.Phases
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