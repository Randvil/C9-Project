using UnityEngine.Events;

public interface ICheckEntrance
{
    public bool EntranceOpen();
    public UnityEvent EntranceOpenEvent { get; }
}
