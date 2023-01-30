using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParryProjectileReflection : ProjectileReflection, IParryDamageEffect
{
    private GameObject gameObject;
    private ITurning turning;

    public UnityEvent SuccessfulParryEvent { get; } = new();

    public ParryProjectileReflection(GameObject gameObject, ITurning turning, ITeam team) : base(team)
    {
        this.gameObject = gameObject;
        this.turning = turning;
    }

    public override void ApplyEffect(Damage incomingDamage)
    {        
        if ((turning.Direction == eDirection.Right && gameObject.transform.position.x - incomingDamage.sourceObject.transform.position.x < 0f) ||
            (turning.Direction == eDirection.Left && gameObject.transform.position.x - incomingDamage.sourceObject.transform.position.x > 0f))
        {
            base.ApplyEffect(incomingDamage);
            SuccessfulParryEvent.Invoke();
        }
    }
}
