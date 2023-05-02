using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCrouchData", menuName = "Data/Components/Crouch/New Crouch Data")]
public class CrouchData : ScriptableObject
{
    [Range(0.01f, 1f)]
    public float colliderSizeMultiplier = 0.4f;
    public SlowEffectData slowEffectData;
}
