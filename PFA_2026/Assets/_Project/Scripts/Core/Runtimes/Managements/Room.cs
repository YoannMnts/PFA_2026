using System.Linq;
using Naussilus.Core.Managements;

namespace Naussilus.Core
{
    public struct Room
    {
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        public Action[] Actions { get; private set; }

        public Room(RoomData data)
        {
            Name = data.Name;
            Description = data.Description;
            Actions = data.Actions.Select(a => new Action(a)).ToArray();
        }
    }
}