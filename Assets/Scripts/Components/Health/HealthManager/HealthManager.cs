using UnityEngine;
using UnityEngine.Events;

public class HealthManager : IHealthManager
{
    private Health health;
    public Health Health => health;

    public UnityEvent<Health> MaxHealthChangedEvent { get; } = new();
    public UnityEvent<Health> CurrentHealthChangedEvent { get; } = new();

    public HealthManager(HealthManagerData healthMangerData)
    {
        health = healthMangerData.initialHealth;
    }

    public void ChangeMaxHealth(float value)
    {
        health.maxHealth = Mathf.Clamp(health.maxHealth + value, 0f, float.MaxValue);
        MaxHealthChangedEvent.Invoke(health);

        ChangeCurrentHealth(0f);
    }

    public void ChangeCurrentHealth(float value)
    {
        health.currentHealth = Mathf.Clamp(health.currentHealth + value, 0f, health.maxHealth);
        CurrentHealthChangedEvent.Invoke(health);
    }

}
