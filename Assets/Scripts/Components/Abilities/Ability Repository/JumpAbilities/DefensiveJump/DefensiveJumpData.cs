using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDefensiveJumpData", menuName = "Data/Abilities/New Defensive Jump Data")]
public class DefensiveJumpData : BaseAbilityData
{
    [Header("Defensive Jump Data")]
    [Min(0f)]
    public float jumpTime;
    [Tooltip("The X component defines the direction. Positive - forward, negative - back.")]
    public Vector2 initialJumpSpeed;
    public AnimationCurve speedCurve;
}
