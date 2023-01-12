using UnityEngine.Events;

public interface IDamageHandler
{
    public void TakeDamage(Damage incomingDamage, IDamageReduced weapon);
    public UnityEvent<float> TakeDamageEvent { get; }
    public UnityEvent DieEvent { get; }
}
