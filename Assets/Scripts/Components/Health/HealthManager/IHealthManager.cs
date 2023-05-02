using UnityEngine.Events;

public interface IHealthManager
{
    public Health Health { get; }
    public void SetMaxHealth(float value);
    public void SetCurrentHealth(float value);
    public void ChangeMaxHealth(float value);
    public void ChangeCurrentHealth(float value);
    public UnityEvent<Health> MaxHealthChangedEvent { get; }
    public UnityEvent<Health> CurrentHealthChangedEvent { get; }
}
