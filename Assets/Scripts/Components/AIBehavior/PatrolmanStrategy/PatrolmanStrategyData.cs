using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSillyPatrolmanData", menuName = "Data/Behavior/New Silly Patrolman Data")]
public class PatrolmanStrategyData : ScriptableObject
{
    [Min(0f)]
    public float searchEnemyDistance = 10f;
    [Min(0f)]
    public float checkPlatformAheadRadius = 0.1f;
    [Min(0f)]
    public float checkWallDistance = 0.1f;
    public LayerMask enemyLayer;
    public LayerMask platformLayer;
}
