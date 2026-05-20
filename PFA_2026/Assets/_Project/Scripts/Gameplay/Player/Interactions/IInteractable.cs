namespace Naussilus.Gameplay.Interactions
{
    public interface IInteractable
    {
        int Priority { get; }
        
        bool IsInteractable() => true;

        void Interact(PlayerInteractions playerInteractions);

    }
}