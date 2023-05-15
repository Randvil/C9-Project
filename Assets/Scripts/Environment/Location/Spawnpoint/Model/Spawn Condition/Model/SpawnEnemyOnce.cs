using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyOnce : SpawnEnemyCondition
{
    private float detectPlayerRadius;
    private float spawnRadius;
    private Spawnpoint spawnpoint;
    private GameObject enemyPrefab;
    private Material material;

    public SpawnEnemyOnce(float detectPlayerRadius, float spawnRadius, Spawnpoint spawnpoint, GameObject enemyPrefab, 
        Material material)
    {
        this.detectPlayerRadius = detectPlayerRadius;
        this.spawnRadius = spawnRadius;
        this.spawnpoint = spawnpoint;
        this.enemyPrefab = enemyPrefab;
        this.material = material;
        spawnEnemyCount = 0;
    }

    public override ISpawnEnemiesConditionData ReturnInfo()
    {
        return new SpawnEnemiesOnceData(detectPlayerRadius, spawnRadius);
    }

    public override void Spawn()
    {
        spawnpoint.StartCoroutine(SpawnEnemyCoroutine());
    }

    public IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitUntil(() => InRadius(spawnpoint, detectPlayerRadius));
        SpawnOneEnemy(enemyPrefab, spawnpoint, material, spawnRadius);
    }

}
