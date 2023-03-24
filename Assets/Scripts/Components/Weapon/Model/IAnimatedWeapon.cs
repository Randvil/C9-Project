using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAnimatedWeapon
{
    public float AttackSpeed { get; }
    public UnityEvent ReleaseAttackEvent { get; }
}
