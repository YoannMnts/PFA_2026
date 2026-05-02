using Naussilus.Core.Consequences;
using Naussilus.Core.Managers;

namespace Naussilus.Core
{
    public struct ConsequenceSide
    {
        public bool IsCurrentNpc { get; private set; }
        
        public INpcSelector Subject { get; private set; }
        
        public IConsequenceEffectValue Stat { get; private set; }
        
        public bool UseEnumerationNpc { get; private set; }
        
        public bool UseRelationshipNpcToReturn { get; private set; }

        public ConsequenceSide(ConsequenceSideData data)
        {
            IsCurrentNpc = data.IsCurrentNpc;
            Subject = IsCurrentNpc ? null : data.Subject?.GetNpcSelector();
            Stat = data.Stat?.GetStat();
            UseEnumerationNpc = Stat is NpcRelationship && data.UseEnumerationNpc;
            UseRelationshipNpcToReturn = Stat is NpcRelationship && data.UseRelationshipNpcToReturn;
        }
    }
}