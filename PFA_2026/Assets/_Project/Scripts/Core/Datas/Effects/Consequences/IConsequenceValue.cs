namespace Naussilus.Core
{
    public interface IConsequenceValue
    {
        public int Amount { get; }

        public abstract void SetNewAmount(int amount);
    }
}