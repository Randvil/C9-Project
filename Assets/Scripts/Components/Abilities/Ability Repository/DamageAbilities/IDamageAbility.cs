using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAbility : IAbility
{
    public float AttackRange { get; }
}
