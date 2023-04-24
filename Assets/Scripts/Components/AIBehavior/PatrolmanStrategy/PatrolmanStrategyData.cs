using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Silly Patrolman Data", menuName = "Component Data/Behavior/New Silly Patrolman Data", order = 610)]
public class PatrolmanStrategyData : ScriptableObject
{
    public float searchEnemyDistance;
    public float checkPlatformAheadRadius;
    public float checkWallDistance;
    public LayerMask enemyLayer;
    public LayerMask platformLayer;
}
