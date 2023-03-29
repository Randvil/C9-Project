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
        health.maxHealth += value;
        MaxHealthChangedEvent.Invoke(health);
    }

    public void ChangeCurrentHealth(float value)
    {
        health.currentHealth += value;
        CurrentHealthChangedEvent.Invoke(health);
    }

}
