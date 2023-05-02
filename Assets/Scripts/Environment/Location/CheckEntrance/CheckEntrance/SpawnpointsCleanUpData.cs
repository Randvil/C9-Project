using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Check Entrance Data", menuName = "Check Entrance Data/Spawnpoints Clean Up Data", order = 170)]
public class SpawnpointsCleanUpData : ScriptableObject
{
    public List<int> CityLocation;
    public List<int> ArcadeCenter;
}

