using UnityEngine.Events;

public class EnergyManager : IEnergyManager
{
    private Energy energy;
    public Energy Energy => energy;

    public UnityEvent<Energy> MaxEnergyChangedEvent { get; } = new();
    public UnityEvent<Energy> CurrentEnergyChangedEvent { get; } = new();

    public EnergyManager(EnergyManagerData energyManagerData)
    {
        energy = energyManagerData.initialEnergy;
    }

    public void ChangeMaxEnergy(float value)
    {
        energy.maxEnergy += value;
        MaxEnergyChangedEvent.Invoke(energy);
    }

    public void ChangeCurrentEnergy(float value)
    {
        energy.currentEnergy += value;
        CurrentEnergyChangedEvent.Invoke(energy);
    }

}
