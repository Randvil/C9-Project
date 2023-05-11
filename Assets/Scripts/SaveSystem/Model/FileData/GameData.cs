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
    public eSceneName sceneName;

    //data about spawnpoints on location
    public List<SpawnpointsOnce> SpawnpointsOnce = new();
    public List<SpawnpointsWave> SpawnpointsWave = new();

    public LocationData(eSceneName sceneName, List<SpawnpointsOnce> spawnpointsOnce, List<SpawnpointsWave> spawnpointsWave)
    {
        this.sceneName = sceneName;
        SpawnpointsOnce = spawnpointsOnce;
        SpawnpointsWave = spawnpointsWave;
    }
}

[Serializable]
public class SpawnpointsOnce
{
    public int id;
    public Vector3 position;
    public int enemyNumber = 1;
    public eSpawnCondition spawnCondition = eSpawnCondition.Once;
    public eEnemyPrefab enemyPrefab;
    public eEnemyMaterials enemyMaterial;
    public SpawnEnemiesOnceData spawnOnceData;

    public SpawnpointsOnce(int id, Vector3 position, eSpawnCondition spawnCondition, eEnemyPrefab enemyPrefab, 
        eEnemyMaterials enemyMaterial, SpawnEnemiesOnceData spawnOnceData)
    {
        this.id = id;
        this.position = position;
        this.spawnCondition = spawnCondition;
        this.enemyPrefab = enemyPrefab;
        this.enemyMaterial = enemyMaterial;
        this.spawnOnceData = spawnOnceData;
    }
}

[Serializable]
public class SpawnpointsWave
{
    public int id;
    public Vector3 position;
    public eSpawnCondition spawnCondition = eSpawnCondition.Wave;
    public eEnemyPrefab enemyPrefab;
    public eEnemyMaterials enemyMaterial;
    public SpawnEnemiesWaveData spawnWaveData;

    public SpawnpointsWave(int id, Vector3 position, eSpawnCondition spawnCondition, eEnemyPrefab enemyPrefab, 
        eEnemyMaterials enemyMaterial, SpawnEnemiesWaveData spawnWaveData)
    {
        this.id = id;
        this.position = position;
        this.spawnCondition = spawnCondition;
        this.enemyPrefab = enemyPrefab;
        this.enemyMaterial = enemyMaterial;
        this.spawnWaveData = spawnWaveData;
    }
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

[Serializable]
public class Data
{
    public float playerHealth;
    public float playerEnergy;
    public Vector3 position;
    public eSceneName latestScene;
    public List<AbilityPair> learnedAbilities;

    public List<LocationData> locations = new();
}

