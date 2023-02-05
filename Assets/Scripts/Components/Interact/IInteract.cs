using UnityEngine.Events;
using UnityEngine;

public interface IInteract
{
    public float InteractRadius { get; }
    public GameObject CheckInteractiveObjectsNear();
}
