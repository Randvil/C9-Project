using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParryProjectileReflection : ProjectileReflection
{
    private GameObject parryingCharacter;
    private ITurning turning;

    public ParryProjectileReflection(ITeam team, float endEffectTime, GameObject parryingCharacter, ITurning turning) : base(team, endEffectTime)
    {
        this.parryingCharacter = parryingCharacter;
        this.turning = turning;
    }

    public override void ApplyEffect(Damage incomingDamage)
    {
        if (incomingDamage.SourceObject == null)
        {
            return;
        }

        bool enemyOnTheRight = parryingCharacter.transform.position.x - incomingDamage.SourceObject.transform.position.x < 0f;

        if ((turning.Direction == eDirection.Right && enemyOnTheRight == true) ||
            (turning.Direction == eDirection.Left && enemyOnTheRight == false))
        {
            base.ApplyEffect(incomingDamage);
        }
    }
}
