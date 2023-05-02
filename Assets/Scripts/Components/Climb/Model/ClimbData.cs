using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClimbData", menuName = "Data/Components/Climb/New Climb Data")]
public class ClimbData : ScriptableObject
{
    [Min(0f)]
    public float speed = 3f;
    [Min(0.01f), Tooltip("How often will the component scan space to find a climbable object [seconds]")]
    public float searchPeriod = 0.1f;
    [Min(0f), Tooltip("Climbable object search radius [units]")]
    public float searchRadius = 0.5f;
    [Min(0f), Tooltip("The character will not be able to climb again during this time after falling from a climbable object [seconds]")]
    public float fallProhibitionPeriod = 0.5f;
    public LayerMask climbableObjectLayer;
}
