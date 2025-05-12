namespace Gameplay
{
    public interface IInteractable
    {
        void Interact(IInteractor target);
        string Name { get; } // FIX: Renamed name to Name so that it doesnt have conflicts with .name
    }
}