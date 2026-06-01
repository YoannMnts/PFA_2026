using System.Linq;
using JetBrains.Annotations;
using Naussilus.Core.Managements.ActionDatas;
using UnityEngine;

namespace Naussilus.Core
{
    public class RoomAction
    {
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        public int Cost { get; private set; }
        
        public int MaxCountdown { get; private set; }
        
        [CanBeNull] public Category[] Categories { get; private set; }
        
        [CanBeNull] public ActionEffect[] ActionEffects { get; private set; }
        
        public bool ClearDefaultSlot { get; private set; }
        
        public bool IsInCountdown { get; private set; }
        public int ActionCountdown { get; private set; }


        public RoomAction(ActionData data)
        {
            Name = data.Name;
            Description = data.Description;
            Cost = data.Cost;
            MaxCountdown = data.Countdown;
            Categories = data.Categories?.Select(c => new Category(c)).ToArray();
            ActionEffects = data.ActionEffects?.Select(a => new ActionEffect(a, Categories)).ToArray();
            ClearDefaultSlot = data.ClearDefaultSlot;
        }
        
        public bool AddOrRemoveCountdown(int value)
        {
            ActionCountdown += value;

            Debug.Log($"AddOrRemoveCountdown: {ActionCountdown} on room {Name}");
            if (ActionCountdown > 0)
            {
                IsInCountdown = true;
                return true;
            }
            
            ActionCountdown = 0;
            IsInCountdown = false;
            return false;

        }
    }
}