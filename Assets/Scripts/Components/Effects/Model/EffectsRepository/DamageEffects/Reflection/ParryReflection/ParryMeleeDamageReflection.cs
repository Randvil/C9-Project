using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParryMeleeDamageReflection : MeleeDamageReflection
{
    private GameObject parryingCharacter;
    private ITurning turning;

    public ParryMeleeDamageReflection(float endEffectTime, GameObject parryingCharacter, ITurning turning, IDamageDealer damageDealer) : base(endEffectTime, damageDealer)
    {
        this.parryingCharacter = parryingCharacter;
        this.turning = turning;
    }

    public override void ApplyEffect(Damage incomingDamage)
    {
        if ((turning.Direction == eDirection.Right && parryingCharacter.transform.position.x - incomingDamage.SourceObject.transform.position.x < 0f)
            || (turning.Direction == eDirection.Left && parryingCharacter.transform.position.x - incomingDamage.SourceObject.transform.position.x > 0f))
        {
            base.ApplyEffect(incomingDamage);
        }
    }
}
