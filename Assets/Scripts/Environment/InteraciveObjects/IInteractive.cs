using UnityEngine;

public interface IInteractive
{
    public void ShowTooltip();
    public void HideTooltip();
    public void StartInteraction(GameObject interactingCharacter);
    public void StopInteraction();
    public void NextStep();
    public bool IsInteracting { get; }
}
