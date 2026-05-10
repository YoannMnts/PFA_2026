using Naussilus.Core.Conditions;
using Naussilus.Core.Managers;

namespace Naussilus.Core
{
    public class ConditionSide
    {
        public bool IsCurrentNpc { get; private set; }
        
        public INpcSelector Subject { get; private set; }
        
        public IConditionEffectValue Stat { get; private set; }

        public bool UseEnumerationNpc { get; private set; }
        
        public bool UseRelationshipNpcToReturn { get; private set; }

        public ConditionSide(ConditionSideData data)
        {
            IsCurrentNpc = data.IsCurrentNpc;
            Subject = IsCurrentNpc ? null : data.Subject?.GetNpcSelector();
            Stat = data.Stat?.GetStat();
            UseEnumerationNpc = Stat is NpcRelationship && data.UseEnumerationNpc;
            UseRelationshipNpcToReturn = Stat is NpcRelationship && data.UseRelationshipNpcToReturn;
        }
    }
}