using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMeleeWeapon : AbstractWeapon, IDamageReduced
{
    [SerializeField]
    protected LayerMask enemyLayerMask;

    [SerializeField]
    protected Damage damageMinus;
    public Damage DamageMinus { get => damageMinus; }
}
