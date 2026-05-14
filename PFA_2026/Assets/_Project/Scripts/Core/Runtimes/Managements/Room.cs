using System.Linq;
using JetBrains.Annotations;
using Naussilus.Core.Managements;

namespace Naussilus.Core
{
    public class Room
    {
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        [CanBeNull] public RoomAction[] Actions { get; private set; }
        
        public int RoomCountdown { get; private set; }
        
        public RoomAction CurrentAction { get; private set; }
        public bool IsIncountdown { get; private set; }
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
            AddOrRemoveCountdown(actions.Countdown);
        }

        public bool AddOrRemoveCountdown(int value)
        {
            RoomCountdown += value;
            
            if (RoomCountdown > 0)
            {
                IsIncountdown = RoomCountdown > 0;
                return true;
            }
            
            CurrentAction = null;
            return false;
        }
    }
}