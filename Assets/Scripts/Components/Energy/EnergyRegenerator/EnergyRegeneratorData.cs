using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnergyRegeneratorData", menuName = "Data/Energy Regeneration/New Energy Regenerator Data")]
public class EnergyRegeneratorData : ScriptableObject
{
    [Min(0f)]
    public float energyPerHit = 1f;
}
