using System.Linq;
using JetBrains.Annotations;
using Naussilus.Core.Managements;

namespace Naussilus.Core
{
    public struct Room
    {
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        [CanBeNull] public RoomAction[] Actions { get; private set; }

        public Room(RoomData data)
        {
            Name = data.Name;
            Description = data.Description;
            Actions = data.Actions?.Select(a => new RoomAction(a)).ToArray();
        }
    }
}