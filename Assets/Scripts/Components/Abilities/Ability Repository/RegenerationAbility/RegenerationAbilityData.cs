using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Regeneration Ability Data", menuName = "Component Data/Model/New Regeneration Ability Data", order = 310)]
public class RegenerationAbilityData : BaseAbilityData
{
    public float healthPerSecond = 10f;
    public float maxRegenerationTime = 5f;
    public float impactPeriod = 0.25f;
}
