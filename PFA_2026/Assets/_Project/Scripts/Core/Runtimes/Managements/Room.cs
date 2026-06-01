using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Naussilus.Core.Managements;
using Naussilus.Gameplay;
using UnityEngine;

namespace Naussilus.Core
{
    public class Room
    {
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        [CanBeNull] public RoomAction[] Actions { get; private set; }
        
        [CanBeNull] public Sprite Icon { get; private set; }
        
        
        public List<RoomAction> CurrentActions { get; private set; }
        public Room(RoomData data)
        {
            Name = data.Name;
            Description = data.Description;
            Actions = data.Actions?.Select(a => new RoomAction(a)).ToArray();
            Icon = data.Icon;
            CurrentActions = new List<RoomAction>();
        }

        public void SetActions(RoomAction actions)
        {
            CurrentActions.Add(actions);
            actions.AddOrRemoveCountdown(actions.MaxCountdown);
        }

        public bool AddOrRemoveAllActionCountdown(int value)
        {
            for (int i = 0; i < CurrentActions.Count; i++)
            {
                var action = CurrentActions[i];
                action.AddOrRemoveCountdown(value);
            }
            
            return true;
        }
    }
}