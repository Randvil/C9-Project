using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Broodmother Strategy Data", menuName = "Component Data/Behavior/New Broodmother Strategy Data", order = 608)]
public class BroodmotherStrategyData : ScriptableObject
{
    public Vector2 recoveryPosition;
    public float phaseTwoThresholdRelativeHealth = 2/3f;
    public float phaseThreeThresholdRelativeHealth = 1/3f;
}
