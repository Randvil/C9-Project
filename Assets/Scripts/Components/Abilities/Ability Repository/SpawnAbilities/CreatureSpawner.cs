using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : AbstractAbility
{
    protected Transform spawnPoint;

    protected GameObject creaturePrefab;
    protected int creatureCount;
    protected float spawnDelay;
    protected int maxCreaturesCount;

    protected List<GameObject> creatures = new();

    public CreatureSpawner(MonoBehaviour owner, Transform spawnPoint, CreatureSpawnerData creatureSpawnerData, IEnergyManager energyManager) : base(owner, creatureSpawnerData, energyManager)
    {
        this.spawnPoint = spawnPoint;

        creaturePrefab = creatureSpawnerData.creaturePrefab;
        creatureCount = creatureSpawnerData.creatureCount;
        spawnDelay = creatureSpawnerData.spawnDelay;
        maxCreaturesCount = creatureSpawnerData.maxCreaturesCount;
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

            List<GameObject> newCreatureList = new();
            foreach(GameObject currentCreature in creatures)
            {
                if (currentCreature != null)
                {
                    newCreatureList.Add(currentCreature);
                }
            }
            creatures = newCreatureList;

            if (creatures.Count >= maxCreaturesCount)
            {
                break;
            }

            GameObject creature = Object.Instantiate(creaturePrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, owner.transform.position.z), Quaternion.identity);
            creatures.Add(creature);
            energyManager.ChangeCurrentEnergy(-cost);

            ReleaseCastEvent.Invoke();
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
