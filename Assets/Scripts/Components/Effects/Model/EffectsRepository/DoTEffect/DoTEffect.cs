using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTEffect : IDoTEffect
{
    IDamageHandler damageHandler;

    public Damage Damage { get; private set; }
    public float DamagePeriod { get; private set; }
    public float LastTickTime { get; set; }
    public float EndEffectTime { get; private set; }


    public DoTEffect(Damage damage, float damagePeriod, float endEffectTime, IDamageHandler damageHandler)
    {
        Damage = damage;
        DamagePeriod = damagePeriod;
        EndEffectTime = endEffectTime;
        this.damageHandler = damageHandler;
    }

    public void DealDamage()
    {
        damageHandler.TakeDamage(Damage);
    }
}
