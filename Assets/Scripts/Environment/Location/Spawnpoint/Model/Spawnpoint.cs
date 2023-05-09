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

    private GameObject EnemyPrefab;
    private eEnemyPrefab enemyType;

    private Material material;
    private eEnemyMaterials materialType;

    private ISpawnEnemyCondition spawnCondition;
    private eSpawnCondition conditionType;

    public void SetSpawnpointInfo(int id, Vector3 position, eEnemyPrefab enemyPrefab, eEnemyMaterials enemyMaterial,
        eSpawnCondition spawnCondition)
    {
        this.id = id;
        this.position = position;
        gameObject.transform.position = position;
        enemyType = enemyPrefab;
        materialType = enemyMaterial;
        conditionType = spawnCondition;

        switch (enemyPrefab)
        {
            case eEnemyPrefab.SpiderBoy:
                EnemyPrefab = prefabsData.spiderBoyPrefab;
                break;
            case eEnemyPrefab.SpiderMinion:
                EnemyPrefab = prefabsData.spiderPrefab;
                break;
            case eEnemyPrefab.FlyingEye:
                EnemyPrefab = prefabsData.flyingEyePrefab;
                break;
            case eEnemyPrefab.Broodmother:
                EnemyPrefab = prefabsData.bossPrefab;
                break;
        }

        switch (enemyMaterial)
        {
            case eEnemyMaterials.DissolveSpiderBoyMAT:
                material = prefabsData.dissolveSpiderBoyMaterial;
                break;
            case eEnemyMaterials.DissolveSpiderMinionMAT:
                material = prefabsData.dissolveSpiderMaterial;
                break;
            case eEnemyMaterials.DissolveFlyingEyeMAT:
                material = prefabsData.dissolveFlyingEyeMaterial;
                break;
            case eEnemyMaterials.DissolveBossMAT:
                material = prefabsData.dissolveBossMaterial;
                break;
        }

    }

    public void SetSpawnpointCondition(float detectPlayerRadius, float spawnRadius)
    {
        spawnCondition = new SpawnEnemyOnce(detectPlayerRadius, spawnRadius, this, EnemyPrefab, material);
        spawnCondition.Spawn();
    }

    public void SetSpawnpointCondition(float detectPlayerRadius, float spawnRadius, int waveCount, int enemyPerWaveCount, 
        float timeBetweenWaves)
    {
        spawnCondition = new SpawnEnemyWave(detectPlayerRadius, spawnRadius, this, EnemyPrefab, material, waveCount, 
            enemyPerWaveCount, timeBetweenWaves);
        spawnCondition.Spawn();
    }

    public void SaveData(Data data)
    {
        string currentSceneName = "";

        foreach (LocationData ld in data.locations)
        {
            switch (ld.sceneName)
            {
                case eSceneName.CityLocation:
                    currentSceneName = "CityLocation";
                    break;
                case eSceneName.ArcadeCenter:
                    currentSceneName = "ArcadeCenter";
                    break;
                case eSceneName.BossLocation:
                    currentSceneName = "BossLocation";
                    break;
            }

            if (currentSceneName == SceneManager.GetActiveScene().name)
            {
                switch (conditionType)
                {
                    case eSpawnCondition.Once:
                        SpawnpointsOnce spawnpointsOnce = new SpawnpointsOnce(id, position, conditionType, enemyType, materialType,
                            spawnCondition.ReturnOnceInfo());
                        foreach (SpawnpointsOnce once in ld.SpawnpointsOnce)
                        {
                            if (once.id == spawnpointsOnce.id)
                            {
                                int enemiesAlive;
                                enemiesAlive = 1 - (spawnCondition.SpawnEnemyCount() - transform.childCount);
                                once.enemyNumber = enemiesAlive;
                            }
                        }
                        break;

                    case eSpawnCondition.Wave:
                        SpawnpointsWave spawnpointsWave = new SpawnpointsWave(id, position, conditionType, enemyType, materialType,
                            spawnCondition.ReturnWaveInfo());
                        foreach (SpawnpointsWave wave in ld.SpawnpointsWave)
                        {
                            if (wave.id == spawnpointsWave.id)
                            {
                                int enemiesAlive;
                                enemiesAlive = wave.spawnWaveData.enemyPerWaveCount * wave.spawnWaveData.waveCount - (spawnCondition.SpawnEnemyCount() - transform.childCount);
                                if (enemiesAlive == 0)
                                    wave.spawnWaveData.enemyPerWaveCount = 0;
                            }
                        }
                        break;
                }
            }
        }
    }

}
