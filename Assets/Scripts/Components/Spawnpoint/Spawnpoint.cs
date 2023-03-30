using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawnpoint : MonoBehaviour, IDataSavable
{
    private int id;
    private Vector3 position;

    //max enemies spawnpoint can spawn
    private int enemyNumber;
    private float killTime;
    private string enemyPrefab;
    private float spawnRadius;
    private string condition;

    private ISpawnEnemyCondition spawnCondition;

    public int Id { get => id; set => id = value; }
    public Vector3 Position { get => position; set => position = value; }
    public int EnemyNumber { get => enemyNumber; set => enemyNumber = value; }
    public float KillTime { get => killTime; set => killTime = value; }
    public string EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    public float SpawnRadius { get => spawnRadius; set => spawnRadius = value; }
    public string Condition { get => condition; set => condition = value; }

    public void StartSpawnpoint(int id, Vector3 position, int enemyNumber, float killTime, string enemyPrefab, float spawnRadius, string condition)
    {
        Id = id;
        Position = position;
        gameObject.transform.position = Position;
        EnemyNumber = enemyNumber;
        KillTime = killTime;
        EnemyPrefab = enemyPrefab;
        SpawnRadius = spawnRadius;
        Condition = condition;

        switch (condition)
        {
            case "once":
                spawnCondition = new SpawnEnemyOnce(spawnRadius, this, enemyPrefab);
                break;
            case "killTime":
                spawnCondition = new SpawnEnemyKillTime(spawnRadius, this, enemyNumber, enemyPrefab, killTime);
                break;
        }

        spawnCondition.Spawn();
    }

    public void SaveData(Data data)
    {
        SpawnpointData spawnpointData = new SpawnpointData(Id, Position, enemyNumber, EnemyPrefab, SpawnRadius, KillTime, Condition);
        foreach (LocationData ld in data.locations)
        {
            if (ld.sceneName == SceneManager.GetActiveScene().name)
            {
                foreach (SpawnpointData sp in ld.spawnpoints)
                {
                    if (sp.id == spawnpointData.id)
                        sp.enemyNumber = spawnpointData.enemyNumber - (spawnCondition.SpawnEnemyCount() - transform.childCount);

                }
            }    
        }
    }

}
