using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Swarm
{
    [Min(0f)]
    public float firstCreatureSpawnDelay = 0f;
    [Min(0f)]
    public float spawnDelay = 1f;
    public Vector2 spawnAreaCenter;
    public Vector2 spawnAreaSize;
    public bool spawnRelativeToCaster = false;
    public GameObject[] swarmMembers;
}
