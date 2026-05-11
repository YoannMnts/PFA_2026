namespace Naussilus.Gameplay.Player.Interactions
{
    //TODO Implementer
    public interface IInteractable
    {
        int Priority { get; }
        
        bool IsInteractable();
        
        void Interact(PlayerInteractions playerInteractions);
    }
}