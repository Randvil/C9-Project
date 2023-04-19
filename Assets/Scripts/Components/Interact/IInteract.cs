public interface IInteract
{
    public void StartInteraction();
    public void BreakInteraction();
    public void NextStep();
    public bool CanInteract { get; }
    public bool IsInteracting { get; }
}
