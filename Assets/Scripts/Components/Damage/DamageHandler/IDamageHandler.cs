using UnityEngine.Events;

public interface IDamageHandler
{
    public void TakeDamage(Damage incomingDamage);
    public UnityEvent<DamageInfo> TakeDamageEvent { get; }
}
