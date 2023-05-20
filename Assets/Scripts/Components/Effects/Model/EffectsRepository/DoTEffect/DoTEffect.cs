using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTEffect : BaseEffect, IDoTEffect
{
    private IDamageHandler damagingCharacterDamageHandler;
    private IDamageDealer damageDealer;

    public Damage Damage { get; private set; }
    public float DamagePeriod { get; private set; }
    public float LastTickTime { get; set; }


    public DoTEffect(GameObject damageOwnerObject, GameObject damageSourceObject, DoTEffectData doTEffectData, IModifierManager damageModifierManager, IDamageHandler damagingCharacterDamageHandler, IDamageDealer damageDealer) : base(doTEffectData)
    {
        Damage = new(damageOwnerObject, damageSourceObject, doTEffectData.damageData, damageModifierManager);
        DamagePeriod = doTEffectData.damagePeriod;
        this.damagingCharacterDamageHandler = damagingCharacterDamageHandler;
        this.damageDealer = damageDealer;
    }

    public void DealDamage()
    {
        damagingCharacterDamageHandler.TakeDamage(Damage, damageDealer);
    }
}
