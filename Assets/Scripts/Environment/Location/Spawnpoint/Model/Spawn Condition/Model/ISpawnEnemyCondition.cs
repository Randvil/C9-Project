using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnEnemyCondition
{
    public int SpawnEnemyCount();
    public void Spawn();
    public SpawnEnemiesWaveData ReturnWaveInfo();
    public SpawnEnemiesOnceData ReturnOnceInfo();
}
