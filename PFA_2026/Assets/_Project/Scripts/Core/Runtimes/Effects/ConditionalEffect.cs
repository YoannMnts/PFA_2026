using System.Linq;
using Naussilus.Core.Managers;

namespace Naussilus.Core
{
    public struct ConditionalEffect
    {
        public bool IsEnumeration { get; private set; }
        
        public INpcSelector CurrentNpcTarget { get; private set; }
        
        public Condition[] Conditions { get; private set; }
        
        public Consequence[] Consequences { get; private set; }

        public ConditionalEffect(ConditionalEffectData data)
        {
            IsEnumeration = data.IsEnumeration;
            CurrentNpcTarget = IsEnumeration ? data.CurrentNpcTarget.GetNpcSelector() : null;
            Conditions = data.Conditions.Select(cond => new Condition(cond)).ToArray();
            Consequences = data.Consequences.Select(cons => new Consequence(cons)).ToArray();
        }
    }
}