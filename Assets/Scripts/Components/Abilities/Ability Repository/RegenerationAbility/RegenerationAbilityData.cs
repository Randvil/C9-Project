using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRegenerationAbilityData", menuName = "Data/Abilities/Regenertaion/New Regeneration Ability Data")]
public class RegenerationAbilityData : BaseAbilityData
{
    [Header("Regeneraion")]
    [Min(0f)]
    public float healthPerSecond = 10f;
    [Min(0f), Tooltip("How long the character can sustain ability [seconds]")]
    public float maxRegenerationTime = 5f;
    [Min(0.01f), Tooltip("How often will energy be converted to health [seconds]")]
    public float impactPeriod = 0.25f;
}
