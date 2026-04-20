namespace Naussilus.Core.Consequences
{
    public interface IConsequenceValue : IConditionalEffect
    {
        public abstract void SetNewAmount(int amount);
    }
}