using UnityEngine;

[CreateAssetMenu(fileName = "NewEnergyManagerData", menuName = "Data/Components/Managers/New Energy Manager Data")]
public class EnergyManagerData : ScriptableObject
{
    public Energy initialEnergy = new()
    {
        maxEnergy = 100f,
        currentEnergy = 100f
    };
}
