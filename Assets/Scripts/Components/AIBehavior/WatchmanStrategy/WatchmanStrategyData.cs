using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWatchmanStrategyData", menuName = "Data/Behavior/New Watchman Strategy Data")]
public class WatchmanStrategyData : ScriptableObject
{
    [Min(0f)]
    public float searchEnemyDistance = 10f;
    [Min(0f)]
    public float turningTimePeriod = 3f;
    public LayerMask enemyLayer;
}
