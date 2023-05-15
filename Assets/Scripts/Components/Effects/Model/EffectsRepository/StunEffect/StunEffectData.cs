using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StunEffectData", menuName = "Data/Effects/New Stun Effect Data")]
public class StunEffectData : ScriptableObject
{
    [Min(0f)]
    public float duration = 1f;
}
