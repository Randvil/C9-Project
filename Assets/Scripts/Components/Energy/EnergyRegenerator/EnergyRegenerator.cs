using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRegenerator : IEnergyRegenerator
{
    //private EnergyRegeneratorData energyRegeneratorData;
    private float energyPerHit = 1f;

    private IEnergyManager energyManager;

    private List<object> prohibitors = new();

    public EnergyRegenerator(EnergyRegeneratorData energyRegeneratorData, IEnergyManager energyManager, IDamageDealer damageDealer)
    {
        this.energyManager = energyManager;
        energyPerHit = energyRegeneratorData.energyPerHit;

        damageDealer.DealDamageEventCallback.AddListener(RestoreEnergy);
    }

    private void RestoreEnergy(DamageInfo damageInfo)
    {
        if (prohibitors.Count > 0)
        {
            return;
        }

        energyManager.ChangeCurrentEnergy(energyPerHit);
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
