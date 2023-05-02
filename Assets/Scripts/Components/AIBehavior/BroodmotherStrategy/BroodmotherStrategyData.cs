using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBroodmotherStrategyData", menuName = "Data/Behavior/New Broodmother Strategy Data")]
public class BroodmotherStrategyData : ScriptableObject
{
    public Vector2 recoveryPosition;
    [Range(0f, 1f), Tooltip("When health drops below this threshold, the character moves on to the second phase [parts of max health]")]
    public float phaseTwoThresholdRelativeHealth = 2/3f;
    [Range(0f, 1f), Tooltip("When health drops below this threshold, the character moves on to the third phase [parts of max health]")]
    public float phaseThreeThresholdRelativeHealth = 1/3f;
}
