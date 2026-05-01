namespace Naussilus.Core
{
    public struct Effect
    {
        public NpcCondition NpcCondition { get; private set; }
        
        public ActionEffect[] ActionsEffects { get; private set; }
    }
}