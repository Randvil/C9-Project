using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawnpoint : MonoBehaviour, IDataSavable
{
    [SerializeField]
    private EnemyPrefabsData prefabsData;

    private int id;
    private Vector3 position;

    //max enemies spawnpoint can spawn
    private int enemyNumber;
    private float timeCountdown;
    private GameObject enemyPrefab;
    private Material enemyMaterial;

    private float spawnRadius;
    private string condition;
    private string enemyName;
    private int waveCount;

    private ISpawnEnemyCondition spawnCondition;

    public int Id { get => id; set => id = value; }
    public Vector3 Position { get => position; set => position = value; }
    public int EnemyNumber { get => enemyNumber; set => enemyNumber = value; }
    public float TimeCountdown { get => timeCountdown; set => timeCountdown = value; }
    public GameObject EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    public float SpawnRadius { get => spawnRadius; set => spawnRadius = value; }
    public string Condition { get => condition; set => condition = value; }
    public string EnemyName { get => enemyName; set => enemyName = value; }
    public int WaveCount { get => waveCount; set => waveCount = value; }
    public Material EnemyMaterial { get => enemyMaterial; set => enemyMaterial = value; }

    public void StartSpawnpoint(int id, Vector3 position, int enemyNumber, float timeCountdown, string enemyPrefab, 
        float spawnRadius, string condition, int waveCount)
    {
        Id = id;
        Position = position;
        gameObject.transform.position = Position;
        EnemyNumber = enemyNumber;
        TimeCountdown = timeCountdown;
        EnemyName = enemyPrefab;
        WaveCount = waveCount;

        switch (enemyPrefab)
        {
            case "SpiderBoy":
                EnemyPrefab = prefabsData.spiderBoyPrefab;
                EnemyMaterial = prefabsData.spiderBoyMaterial;
                break;
            case "SpiderMinion":
                EnemyPrefab = prefabsData.spiderPrefab;
                EnemyMaterial = prefabsData.spiderMaterial;
                break;
            case "FlyingEye":
                EnemyPrefab = prefabsData.flyingEyePrefab;
                EnemyMaterial = prefabsData.flyingEyeMaterial;
                break;
            case "Boss":
                EnemyPrefab = prefabsData.bossPrefab;
                EnemyMaterial = prefabsData.bossMaterial;
                break;
        }

        SpawnRadius = spawnRadius;
        Condition = condition;

        switch (condition)
        {
            case "once":
                spawnCondition = new SpawnEnemyOnce(spawnRadius, this, EnemyPrefab, EnemyMaterial);
                break;
            case "killTime":
                spawnCondition = new SpawnEnemyKillTime(spawnRadius, this, enemyNumber, EnemyPrefab, timeCountdown, EnemyMaterial);
                break;
            case "wave":
                spawnCondition = new SpawnEnemyWave(spawnRadius, this, EnemyPrefab, waveCount, enemyNumber, timeCountdown, EnemyMaterial);
                break;
        }

        spawnCondition.Spawn();
    }

    public void SaveData(Data data)
    {
        SpawnpointData spawnpointData = new SpawnpointData(Id, Position, enemyNumber, EnemyName, SpawnRadius, Condition, TimeCountdown, WaveCount);
        foreach (LocationData ld in data.locations)
        {
            if (ld.sceneName == SceneManager.GetActiveScene().name)
            {
                foreach (SpawnpointData sp in ld.spawnpoints)
                {
                    if (sp.id == spawnpointData.id)
                    {
                        int killedEnemies = spawnpointData.enemyNumber - (spawnCondition.SpawnEnemyCount() - transform.childCount);
                        if (killedEnemies < 0)
                            sp.enemyNumber = 0;
                        else
                            sp.enemyNumber = killedEnemies;
                    }
                }
            }    
        }
    }

}
