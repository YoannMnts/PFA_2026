using System;
using System.Collections.Generic;
using System.Linq;
using Naussilus.Core.Managements.ActionDatas;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.Managers.Rooms;
using UnityEngine;

namespace Naussilus.Core
{
    public class Category
    {
        public string Name { get; private set; }
        
        public int Quantity { get; private set; }
        
        public Npc[] ProhibitedNpcs { get; private set; }
        
        public Npc[] ObligateNpcs { get; private set; }
        
        public Npc[] CurrentNpcs { get; private set; }

        public Category(CategoryData data)
        {
            Name = data.Name;
            Quantity = data.Quantity;
            ProhibitedNpcs = data.ProhibitedNpc?.Select(npc => NpcManager.TryGetNpc(npc.GUID)).ToArray();
            ObligateNpcs = data.ObligateNpc?.Select(npc => NpcManager.TryGetNpc(npc.GUID)).ToArray();
            CurrentNpcs = this.SetDefaultCurrentNpcs();
        }

        public void ClearCurrentNpcs()
        {
            CurrentNpcs = null;
            CurrentNpcs = this.SetDefaultCurrentNpcs();
        }
    }
}