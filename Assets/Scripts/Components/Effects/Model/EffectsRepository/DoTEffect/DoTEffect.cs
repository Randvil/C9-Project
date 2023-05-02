using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTEffect : IDoTEffect
{
    private IDamageHandler damagingCharacterDamageHandler;
    private IDamageDealer damageDealer;

    public Damage Damage { get; private set; }
    public float DamagePeriod { get; private set; }
    public float LastTickTime { get; set; }
    public float EndEffectTime { get; private set; }


    public DoTEffect(GameObject damageOwnerObject, GameObject damageSourceObject, DoTEffectData doTEffectData, IModifierManager damageModifierManager, IDamageHandler damagingCharacterDamageHandler, IDamageDealer damageDealer)
    {
        Damage = new(damageOwnerObject, damageSourceObject, doTEffectData.damageData, damageModifierManager);
        DamagePeriod = doTEffectData.damagePeriod;
        EndEffectTime = Time.time + doTEffectData.duration;
        this.damagingCharacterDamageHandler = damagingCharacterDamageHandler;
        this.damageDealer = damageDealer;
    }

    public void DealDamage()
    {
        damagingCharacterDamageHandler.TakeDamage(Damage, damageDealer);
    }
}
