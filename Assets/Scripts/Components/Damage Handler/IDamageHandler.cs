using UnityEngine.Events;

public interface IDamageHandler
{
    public void TakeDamage(Damage incomingDamage);
    public UnityEvent<float> TakeDamageEvent { get; }
    public UnityEvent DieEvent { get; }
}
