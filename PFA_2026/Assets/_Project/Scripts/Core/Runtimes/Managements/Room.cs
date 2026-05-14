using System.Linq;
using JetBrains.Annotations;
using Naussilus.Core.Managements;
using UnityEngine;

namespace Naussilus.Core
{
    public class Room
    {
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        [CanBeNull] public RoomAction[] Actions { get; private set; }
        
        public int RoomCountdown { get; private set; }
        
        public RoomAction CurrentAction { get; private set; }
        public bool IsInCountdown { get; private set; }
        public Room(RoomData data)
        {
            Name = data.Name;
            Description = data.Description;
            Actions = data.Actions?.Select(a => new RoomAction(a)).ToArray();
            RoomCountdown = 0;
        }

        public void SetActions(RoomAction actions)
        {
            CurrentAction = actions;
            RoomCountdown = actions.Countdown;
            IsInCountdown = true;
        }

        public bool AddOrRemoveCountdown(int value)
        {
            if (CurrentAction == null)
                return false;
            
            RoomCountdown += value;

            Debug.Log($"AddOrRemoveCountdown: {RoomCountdown} on room {Name}");
            if (RoomCountdown > 0) 
                return true;
            
            CurrentAction = null;
            RoomCountdown = 0;
            IsInCountdown = false;
            return false;

        }
    }
}