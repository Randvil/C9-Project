using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Watchman Strategy Data", menuName = "Component Data/Behavior/New Watchman Strategy Data", order = 609)]
public class WatchmanStrategyData : ScriptableObject
{
    public float searchEnemyDistance = 10f;
    public float turningTimePeriod = 3f;
    public LayerMask enemyLayer;
}
