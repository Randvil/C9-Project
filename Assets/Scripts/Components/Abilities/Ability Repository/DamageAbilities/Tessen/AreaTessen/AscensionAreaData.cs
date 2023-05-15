using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AscensionAreaData", menuName = "Data/Abilities/Area Tessen/New Ascension Area Data")]
public class AscensionAreaData : ScriptableObject
{
    [Min(0f)]
    public float lifetime = 3f;
    [Min(0.01f), Tooltip("How often unaffected characters will be affected by the ability, and those already affected will receive damage [seconds]")]
    public float impactPeriod = 0.25f;
    [Min(0f), Tooltip("Vertical speed of hovering character [units per second]")]
    public float ascensionalPower = 2f;
    public StunEffectData stunEffectData;
    public DamageData damageData;
}
