using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTEffect : IDoTEffect
{
    private IDamageHandler damageHandler;
    private IDamageDealer damageDealer;

    public Damage Damage { get; private set; }
    public float DamagePeriod { get; private set; }
    public float LastTickTime { get; set; }
    public float EndEffectTime { get; private set; }


    public DoTEffect(Damage damage, float damagePeriod, float endEffectTime, IDamageHandler damageHandler, IDamageDealer damageDealer)
    {
        Damage = damage;
        DamagePeriod = damagePeriod;
        EndEffectTime = endEffectTime;
        this.damageHandler = damageHandler;
        this.damageDealer = damageDealer;
    }

    public void DealDamage()
    {
        damageHandler.TakeDamage(Damage, damageDealer.DealDamageEvent);
    }
}
