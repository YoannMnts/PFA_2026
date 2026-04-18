namespace Naussilus.Core.Consequences
{
    public interface IConsequenceValue
    {
        public int Amount { get; }

        public abstract void SetNewAmount(int amount);
    }
}