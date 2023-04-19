using UnityEngine;

[CreateAssetMenu(fileName = "New Health Manager Data", menuName = "Component Data/Model/New Health Manager Data", order = 110)]
public class HealthManagerData : ScriptableObject
{
    public Health initialHealth = new()
    {
        maxHealth = 100f,
        currentHealth = 100f
    };
}
