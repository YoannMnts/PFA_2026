using System.Linq;
using JetBrains.Annotations;
using Naussilus.Core.Managements.ActionDatas;

namespace Naussilus.Core
{
    public class RoomAction
    {
        public string Name { get; private set; }
        
        public int Cost { get; private set; }
        
        public int Countdown { get; private set; }
        
        [CanBeNull] public Category[] Categories { get; private set; }
        
        [CanBeNull] public ActionEffect[] ActionEffects { get; private set; }

        public RoomAction(ActionData data)
        {
            Name = data.Name;
            Cost = data.Cost;
            Countdown = data.Countdown;
            Categories = data.Categories?.Select(c => new Category(c)).ToArray();
            ActionEffects = data.ActionEffects?.Select(a => new ActionEffect(a, Categories)).ToArray();
        }
    }
}