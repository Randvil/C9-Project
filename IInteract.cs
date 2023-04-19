using UnityEngine.Events;
using UnityEngine;

public interface IInteract
{
    public void StartInteraction();
    public void StopInteraction();
    public void NextStep();
}
