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

    public void SetMaxHealth(float value)
    {
        health.maxHealth = value;
        MaxHealthChangedEvent.Invoke(health);

        if (health.currentHealth > health.maxHealth)
        {
            SetCurrentHealth(value);
        }
    }

    public void SetCurrentHealth(float value)
    {
        health.currentHealth = Mathf.Clamp(value, 0f, health.maxHealth);
        CurrentHealthChangedEvent.Invoke(health);
    }

    public void ChangeMaxHealth(float value)
    {
        if (value == 0f)
        {
            return;
        }

        health.maxHealth = Mathf.Clamp(health.maxHealth + value, 0f, float.MaxValue);
        MaxHealthChangedEvent.Invoke(health);

        if (health.currentHealth > health.maxHealth)
        {
            SetCurrentHealth(value);
        }
    }

    public void ChangeCurrentHealth(float value)
    {
        if (value == 0f)
        {
            return;
        }

        health.currentHealth = Mathf.Clamp(health.currentHealth + value, 0f, health.maxHealth);
        CurrentHealthChangedEvent.Invoke(health);
    }
}
