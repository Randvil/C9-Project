using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyKillTime : SpawnEnemyCondition
{
    private float spawnRadius;
    private Spawnpoint spawnpoint;
    private int maxEnemyNumber;
    private GameObject enemyPrefab;
    private Material material;
    private float currCountdownValue;
    private float killTime;

    public SpawnEnemyKillTime(float spawnRadius, Spawnpoint spawnpoint, int maxEnemyNumber, GameObject enemyPrefab, float killTime, Material material)
    {
        this.spawnRadius = spawnRadius;
        this.spawnpoint = spawnpoint;
        spawnEnemyCount = 0;
        this.maxEnemyNumber = maxEnemyNumber;
        this.enemyPrefab = enemyPrefab;
        this.killTime = killTime;
        this.material = material;
    }

    public override void Spawn()
    {
        spawnpoint.StartCoroutine(SpawnEnemyCoroutine(killTime));
    }

    public IEnumerator SpawnEnemyCoroutine(float countdownValue)
    {
        yield return new WaitUntil(() => InRadius(spawnpoint, spawnRadius));
        SpawnOneEnemy(enemyPrefab, spawnpoint, material);

        while (spawnEnemyCount != maxEnemyNumber)
        {
            yield return StartCountdown(countdownValue);
            if (spawnpoint.transform.childCount != 0)
                SpawnOneEnemy(enemyPrefab, spawnpoint, material);
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
