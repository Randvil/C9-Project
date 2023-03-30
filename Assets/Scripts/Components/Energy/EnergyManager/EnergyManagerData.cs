using UnityEngine;

[CreateAssetMenu(fileName = "New Energy Manager Data", menuName = "Component Data/Model/New Energy Manager Data", order = 120)]
public class EnergyManagerData : ScriptableObject
{
    public Energy initialEnergy = new()
    {
        maxEnergy = 100f,
        currentEnergy = 100f
    };
}
