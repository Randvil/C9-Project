using System;
using UnityEngine.Events;

public interface IDamageHandler
{
    public void TakeDamage(Damage incomingDamage, UnityEvent<DamageInfo> dealDamageEvent);
    public UnityEvent<DamageInfo> TakeDamageEvent { get; }
}
