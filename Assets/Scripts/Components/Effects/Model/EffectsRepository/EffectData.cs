using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CommonEffectData", menuName = "Data/Effects/New Common Effect Data")]
public class EffectData : ScriptableObject
{
    [Tooltip("Effect will not be applied to an enemy with a higher susceptibility than its power")]
    public eEffectPower effectPower = eEffectPower.Weak;
    [Min(0f)]
    public float duration = 1f;
}
