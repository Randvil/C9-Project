using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameData
{
    public Data CheckpointData = new();

    //data about locations for moving between scenes
    public Data CurrentGameData = new();
}

[Serializable]
public class LocationData
{
    public string sceneName;

    //data about spawnpoints on location
    public List<SpawnpointData> spawnpoints = new();

    public LocationData(string sceneName, List<SpawnpointData> spawnpoints)
    {
        this.sceneName = sceneName;
        this.spawnpoints = spawnpoints;
    }
}

[Serializable]
public class SpawnpointData
{
    public int id;
    public Vector3 position;
    public int enemyNumber;
    public string enemyPrefabName;
    public float spawnRadius;
    public float killTime;
    public string condition;

    public SpawnpointData(int id, Vector3 position, int enemyNumber, string enemyPrefabName, float spawnRadius, float killTime, 
        string condition)
    {
        this.id = id;
        this.position = position;
        this.enemyNumber = enemyNumber;
        this.enemyPrefabName = enemyPrefabName;
        this.spawnRadius = spawnRadius;
        this.killTime = killTime;
        this.condition = condition;
    }
}

[Serializable]
public class Data
{
    public float playerHealth;
    public Vector3 position;
    public string scene;

    public List<LocationData> locations = new();
}