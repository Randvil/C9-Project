using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyKillTime : SpawnEnemyCondition
{
    private float spawnRadius;
    private Spawnpoint spawnpoint;
    private int maxEnemyNumber;
    private string enemyPrefab;
    private float currCountdownValue;
    private float killTime;

    public SpawnEnemyKillTime(float spawnRadius, Spawnpoint spawnpoint, int maxEnemyNumber, string enemyPrefab, float killTime)
    {
        this.spawnRadius = spawnRadius;
        this.spawnpoint = spawnpoint;
        spawnEnemyCount = 0;
        this.maxEnemyNumber = maxEnemyNumber;
        this.enemyPrefab = enemyPrefab;
        this.killTime = killTime;
    }

    public override void Spawn()
    {
        spawnpoint.StartCoroutine(SpawnEnemyCoroutine(killTime));
    }

    public void SpawnEnemy()
    {
        GameObject prefab = Resources.Load<GameObject>(enemyPrefab);
        GameObject instantiate = Object.Instantiate(prefab, spawnpoint.transform.position, Quaternion.identity, spawnpoint.transform);
        instantiate.name = enemyPrefab;
        spawnEnemyCount++;
    }

    public IEnumerator SpawnEnemyCoroutine(float countdownValue)
    {
        yield return new WaitUntil(() => InRadius(spawnpoint, spawnRadius));
        SpawnEnemy();
        while (spawnpoint.transform.childCount != 0 && spawnEnemyCount != maxEnemyNumber)
        {
            yield return StartCountdown(countdownValue);
            SpawnEnemy();
        }  
        yield return null;
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
