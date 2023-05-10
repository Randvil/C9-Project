using System;

[Serializable]
public class SpawnEnemiesOnceData: ISpawnEnemiesConditionData
{
    public float detectPlayerRadius;
    public float spawnRadius;

    public SpawnEnemiesOnceData(float detectPlayerRadius, float spawnRadius)
    {
        this.detectPlayerRadius = detectPlayerRadius;
        this.spawnRadius = spawnRadius;
    }
}
