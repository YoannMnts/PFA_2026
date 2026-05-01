using System.Linq;
using Naussilus.Core.Managements.ActionDatas;

namespace Naussilus.Core
{
    public struct Action
    {
        public string Name { get; private set; }
        
        public int Cost { get; private set; }
        
        public int Countdown { get; private set; }
        
        public Category[] Categories { get; private set; }
        
        public ActionEffect[] ActionEffects { get; private set; }

        public Action(ActionData data)
        {
            Name = data.Name;
            Cost = data.Cost;
            Countdown = data.Countdown;
            Categories = data.Categories.Select(c => new Category(c)).ToArray();
            ActionEffects = data.ActionEffects.Select(a => new ActionEffect(a)).ToArray();
        }
    }
}