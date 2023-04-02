using UnityEngine;
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
        energy.maxEnergy = Mathf.Clamp(energy.maxEnergy + value, 0f, float.MaxValue);
        MaxEnergyChangedEvent.Invoke(energy);

        ChangeCurrentEnergy(0f);
    }

    public void ChangeCurrentEnergy(float value)
    {
        energy.currentEnergy = Mathf.Clamp(energy.currentEnergy + value, 0f, energy.maxEnergy);
        CurrentEnergyChangedEvent.Invoke(energy);
    }

}
