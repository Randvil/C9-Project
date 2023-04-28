using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gravity Data", menuName = "Component Data/Model/New Gravity Data")]
public class GravityData : ScriptableObject
{
    public LayerMask groundLayer;
    public AnimationCurve fallSpeedCurve;
    [Min(0f)]
    public float maxFallSpeed = 2f;
    [Min(0f)]
    public float maxSpeedTime = 1f;
}
