using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRegenerator : IEnergyRegenerator
{
    private EnergyRegeneratorData energyRegeneratorData;

    private IEnergyManager energyManager;

    private List<object> prohibitors = new();

    public EnergyRegenerator(EnergyRegeneratorData energyRegeneratorData, IEnergyManager energyManager, IDamageDealer damageDealer)
    {
        this.energyManager = energyManager;
        this.energyRegeneratorData = energyRegeneratorData;

        damageDealer.DealDamageEvent.AddListener(RestoreEnergy);
    }

    private void RestoreEnergy(DamageInfo damageInfo)
    {
        if (prohibitors.Count > 0)
        {
            return;
        }

        energyManager.ChangeCurrentEnergy(energyRegeneratorData.energyPerHit);
        Debug.Log($"{energyRegeneratorData.energyPerHit} energy were added");
    }

    public void AllowRegeneration(object allower)
    {
        prohibitors.Remove(allower);
    }

    public void ProhibitRegeneration(object prohibitor)
    {
        prohibitors.Add(prohibitor);
    }

    public void RemoveAllProhibitors()
    {
        prohibitors.Clear();
    }

}
