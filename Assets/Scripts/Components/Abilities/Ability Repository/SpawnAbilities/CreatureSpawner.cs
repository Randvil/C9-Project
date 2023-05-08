using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : AbstractAbility
{
    protected Transform spawnPoint;

    protected GameObject creaturePrefab;
    protected int creatureCount;
    protected float spawnDelay;

    public CreatureSpawner(MonoBehaviour owner, Transform spawnPoint, CreatureSpawnerData creatureSpawnerData, IEnergyManager energyManager) : base(owner, creatureSpawnerData, energyManager)
    {
        this.spawnPoint = spawnPoint;

        creaturePrefab = creatureSpawnerData.creaturePrefab;
        creatureCount = creatureSpawnerData.creatureCount;
        spawnDelay = creatureSpawnerData.spawnDelay;
    }

    protected override IEnumerator ReleaseStrikeCoroutine()
    {
        finishCooldownTime = Time.time + cooldown;        

        for (int i = 0; i < creatureCount; i++)
        {
            if (energyManager.Energy.currentEnergy < cost)
            {
                break;
            }

            Object.Instantiate(creaturePrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z), Quaternion.identity);
            energyManager.ChangeCurrentEnergy(-cost);

            ReleaseCastEvent.Invoke();
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
