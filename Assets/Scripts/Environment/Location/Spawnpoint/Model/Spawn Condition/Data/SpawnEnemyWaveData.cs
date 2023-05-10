using System;

[Serializable]
public class SpawnEnemiesWaveData : ISpawnEnemiesConditionData
{
    public float detectPlayerRadius;
    public float spawnRadius;
    public int waveCount;
    public int enemyPerWaveCount;
    public float timeBetweenWaves;

    public SpawnEnemiesWaveData(float detectPlayerRadius, float spawnRadius, int waveCount, int enemyPerWaveCount, 
        float timeBetweenWaves)
    {
        this.detectPlayerRadius = detectPlayerRadius;
        this.spawnRadius = spawnRadius;
        this.waveCount = waveCount;
        this.enemyPerWaveCount = enemyPerWaveCount;
        this.timeBetweenWaves = timeBetweenWaves;
    }
}
