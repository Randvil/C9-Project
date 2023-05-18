using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CommonEffectData", menuName = "Data/Effects/New Common Effect Data")]
public class EffectData : ScriptableObject
{
    public eEffectPower effectPower = eEffectPower.Weak;
    [Min(0f)]
    public float duration = 1f;
}
