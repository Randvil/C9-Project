using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthManagerData", menuName = "Data/Components/Managers/New Health Manager Data")]
public class HealthManagerData : ScriptableObject
{
    public Health initialHealth = new()
    {
        maxHealth = 100f,
        currentHealth = 100f
    };
}
