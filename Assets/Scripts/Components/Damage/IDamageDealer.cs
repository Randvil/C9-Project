using UnityEngine.Events;

public interface IDamageDealer
{
    public UnityEvent<DamageInfo> DealDamageEvent { get; }
}
