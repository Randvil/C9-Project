using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyOnce : SpawnEnemyCondition
{
    private float spawnRadius;
    private Spawnpoint spawnpoint;
    private string enemyPrefab;

    public SpawnEnemyOnce(float spawnRadius, Spawnpoint spawnpoint, string enemyPrefab)
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
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        GameObject prefab = Resources.Load<GameObject>(enemyPrefab);
        GameObject instantiate = Object.Instantiate(prefab, spawnpoint.transform.position, Quaternion.identity, spawnpoint.transform);
        instantiate.name = enemyPrefab;
        spawnEnemyCount++;
    }

}
