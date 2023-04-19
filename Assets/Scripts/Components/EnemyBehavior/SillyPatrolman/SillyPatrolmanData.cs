using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Silly Patrolman Data", menuName = "Component Data/Model/New Silly Patrolman Data", order = 610)]
public class SillyPatrolmanData : ScriptableObject
{
    public float senseDelay;
    public float thinkDelay;
    public float actDelay;
    public float searchPlayerDistance;
    public float loseSightOfPlayerDistance;
    public float checkPlatformAheadRadius;
    public float checkWallDistance;
    public float attackRadius;
    public LayerMask enemyLayerMask;
    public LayerMask platformLayerMask;
}
