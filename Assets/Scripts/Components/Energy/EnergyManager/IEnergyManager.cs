using UnityEngine.Events;

public interface IEnergyManager
{
    public Energy Energy { get; }
    public void ChangeMaxEnergy(float value);
    public void ChangeCurrentEnergy(float value);
    public UnityEvent<Energy> MaxEnergyChangedEvent { get; }
    public UnityEvent<Energy> CurrentEnergyChangedEvent { get; }
}
