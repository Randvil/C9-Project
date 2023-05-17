using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RootEffectData", menuName = "Data/Effects/New Root Effect Data")]
public class RootEffectData : ScriptableObject
{
    [Min(0f)]
    public float duration = 1f;
}
