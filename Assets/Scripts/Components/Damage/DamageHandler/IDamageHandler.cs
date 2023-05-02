using System;
using UnityEngine.Events;

public interface IDamageHandler
{
    public void TakeDamage(Damage incomingDamage, IDamageDealer damageDealer);
    public UnityEvent<DamageInfo> TakeDamageEvent { get; }
}
