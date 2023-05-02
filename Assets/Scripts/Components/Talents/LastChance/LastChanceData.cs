using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLastChanceData", menuName = "Data/Talents/Last Chance/New Last Chance Data")]
public class LastChanceData : ScriptableObject
{
    [Min(1)]
    public int initialCharges = 1;
    [Min(0f)]
    public float invincibilityDuration = 1f;
}
