using UnityEngine.Events;

public interface IHorrorManager
{
    public Horror Horror { get; }
    public void ChangeMaxHorror(float value);
    public void ChangeCurrentHorror(float value);
    public UnityEvent<Horror> MaxHorrorChangedEvent { get; }
    public UnityEvent<Horror> CurrentHorrorChangedEvent { get; }
}
