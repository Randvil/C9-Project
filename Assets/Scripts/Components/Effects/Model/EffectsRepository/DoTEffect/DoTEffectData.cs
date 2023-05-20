using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoTEffectData", menuName = "Data/Effects/New DoT Effect Data")]
public class DoTEffectData : EffectData
{
    [Tooltip("Damage per second")]
    public DamageData damageData;
    [Min(0.01f), Tooltip("How often the damage will be dealt [seconds]. Does not affect damage value")]
    public float damagePeriod = 0.25f;
}