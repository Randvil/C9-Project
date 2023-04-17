using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyOnce : SpawnEnemyCondition
{
    private float spawnRadius;
    private Spawnpoint spawnpoint;
    private GameObject enemyPrefab;

    public SpawnEnemyOnce(float spawnRadius, Spawnpoint spawnpoint, GameObject enemyPrefab)
    {
        this.spawnRadius = spawnRadius;
        this.spawnpoint = spawnpoint;
        this.enemyPrefab = enemyPrefab;
        spawnEnemyCount = 0;
    }

    public override void Spawn()
    {
        spawnpoint.StartCoroutine(SpawnEnemyCoroutine());
    }

    public IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitUntil(() => InRadius(spawnpoint, spawnRadius));
        SpawnOneEnemy(enemyPrefab, spawnpoint);
    }

}
