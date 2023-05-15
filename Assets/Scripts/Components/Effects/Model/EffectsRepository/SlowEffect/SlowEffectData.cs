using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlowEffectData", menuName = "Data/Effects/New Slow Effect Data")]
public class SlowEffectData : ScriptableObject
{
    [Range(0f, 1f), Tooltip("How much the character will be slowed down [parts of movement speed]. Stacks multiplicative.")]
    public float movementSlowValue;
    [Min(0f)]
    public float duration;
    public Material webMaterial;
}
