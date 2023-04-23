using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameData
{
    public Data CheckpointData = new();

    //data about locations for moving between scenes
    public Data CurrentGameData = new();

    public GameData()
    {
    }

    public GameData(Data checkpointData, Data currentGameData)
    {
        CheckpointData = checkpointData;
        CurrentGameData = currentGameData;
    }
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
    public string condition;

    public float timeCountdown;
    public int waveCount;

    public SpawnpointData(int id, Vector3 position, int enemyNumber, string enemyPrefabName, float spawnRadius, string condition, 
        float timeCountdown, int waveCount)
    {
        this.id = id;
        this.position = position;
        this.enemyNumber = enemyNumber;
        this.enemyPrefabName = enemyPrefabName;
        this.spawnRadius = spawnRadius;
        this.condition = condition;
        this.timeCountdown = timeCountdown;
        this.waveCount = waveCount;
    }
}

[Serializable]
public class Data
{
    public float playerHealth;
    public float playerEnergy;
    public Vector3 position;
    public string latestScene;
    public List<AbilityPair> learnedAbilities;

    public List<LocationData> locations = new();
}

[Serializable]
public class AbilityPair
{
    public int pos;
    public eAbilityType abilityType;

    public AbilityPair(int pos, eAbilityType abilityType)
    {
        this.pos = pos;
        this.abilityType = abilityType;
    }
}