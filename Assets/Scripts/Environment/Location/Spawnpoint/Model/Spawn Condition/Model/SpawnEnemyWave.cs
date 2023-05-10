using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyWave : SpawnEnemyCondition
{
    private float detectPlayerRadius;
    private float spawnRadius;
    private Spawnpoint spawnpoint;
    private GameObject enemyPrefab;
    private Material material;
    private int waveCount;
    private int enemyPerWaveCount;
    private float timeBetweenWaves;
    private float currCountdownValue;

    public SpawnEnemyWave(float detectPlayerRadius, float spawnRadius, Spawnpoint spawnpoint, GameObject enemyPrefab, 
        Material material, int waveCount, int enemyPerWaveCount, float timeBetweenWaves)
    {
        this.detectPlayerRadius = detectPlayerRadius;
        this.spawnRadius = spawnRadius;
        this.spawnpoint = spawnpoint;
        this.enemyPrefab = enemyPrefab;
        this.material = material;
        this.waveCount = waveCount;
        this.enemyPerWaveCount = enemyPerWaveCount;
        this.timeBetweenWaves = timeBetweenWaves;
        spawnEnemyCount = 0;
    }

    public override ISpawnEnemiesConditionData ReturnInfo()
    {
        return new SpawnEnemiesWaveData(detectPlayerRadius, spawnRadius, waveCount, enemyPerWaveCount, timeBetweenWaves);
    }

    public override void Spawn()
    {
        spawnpoint.StartCoroutine(SpawnCoroutine(timeBetweenWaves));
    }

    public IEnumerator SpawnCoroutine(float timeBetweenWaves)
    {
        yield return new WaitUntil(() => InRadius(spawnpoint, detectPlayerRadius));
        spawnpoint.StartCoroutine(SpawnOneWave());

        for (int i = 1; i < waveCount; i++)
        {
            yield return StartCountdown(timeBetweenWaves);
            spawnpoint.StartCoroutine(SpawnOneWave());
        }

        yield return null;
    }

    public IEnumerator SpawnOneWave()
    {
        for (int i = 0; i < enemyPerWaveCount; i++)
        {
            yield return new WaitForSeconds(0.8f);
            SpawnOneEnemy(enemyPrefab, spawnpoint, material, spawnRadius);
        }
    }

    public IEnumerator StartCountdown(float countdownValue)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            //Debug.Log(currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
    }
}
