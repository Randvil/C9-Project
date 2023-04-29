using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Defensive Jump Data", menuName = "Component Data/Model/New Defensive Jump Data", order = 311)]
public class DefensiveJumpData : BaseAbilityData
{
    [Header("Defensive Jump Data")]
    public float jumpTime;
    public Vector2 initialJumpSpeed;
    public AnimationCurve speedCurve;
}
