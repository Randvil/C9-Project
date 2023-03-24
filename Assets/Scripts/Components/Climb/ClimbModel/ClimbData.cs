using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Climb Data", menuName = "Component Data/Model/New Climb Data", order = 170)]
public class ClimbData : ScriptableObject
{
    public float speed = 3f;
    public float searchPeriod = 0.1f;
    public float searchRadius = 0.5f;
    public float fallProhibitionPeriod = 0.5f;
    public LayerMask climbableObjectLayer;
}
