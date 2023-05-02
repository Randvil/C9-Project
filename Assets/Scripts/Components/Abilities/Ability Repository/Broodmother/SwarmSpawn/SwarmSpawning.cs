using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSpawning : AbstractAbility
{
    protected float spawnDuration;
    protected Swarm[] swarms;

    public SwarmSpawning(MonoBehaviour owner, SwarmSpawningData swarmSpawningData, IEnergyManager energyManager) : base(owner, swarmSpawningData, energyManager)
    {
        spawnDuration = swarmSpawningData.spawnDuration;
        swarms = swarmSpawningData.swarms;
    }

    protected override IEnumerator ReleaseStrikeCoroutine()
    {
        yield return new WaitForSeconds(preCastDelay);

        energyManager.ChangeCurrentEnergy(-cost);

        float startTime = Time.time;
        Dictionary<Swarm, float> swarmSpawnTimes = new();
        foreach (Swarm swarm in swarms)
        {
            swarmSpawnTimes.Add(swarm, startTime + swarm.firstCreatureSpawnDelay);
        }

        while (Time.time < startTime + spawnDuration)
        {
            Debug.Log("Spawning");
            float currentTime = Time.time;

            Dictionary<Swarm, float> newTimes = new();
            foreach (KeyValuePair<Swarm, float> swarmSpawnTime in swarmSpawnTimes)
            {
                Swarm currentSwarm = swarmSpawnTime.Key;
                float lastSpawnTime = swarmSpawnTime.Value;

                if (currentTime < lastSpawnTime + currentSwarm.spawnDelay)
                {
                    newTimes.Add(currentSwarm, lastSpawnTime);
                    continue;
                }

                GameObject spawnCreature = currentSwarm.swarmMembers[Random.Range(0, currentSwarm.swarmMembers.Length)];

                float xSpawnAreaPosition = Random.Range(-currentSwarm.spawnAreaSize.x, currentSwarm.spawnAreaSize.x);
                float ySpawnAreaPosition = Random.Range(-currentSwarm.spawnAreaSize.y, currentSwarm.spawnAreaSize.y);
                Vector2 relativeCasterPosition = currentSwarm.spawnRelativeToCaster ? owner.gameObject.transform.position : Vector2.zero;
                Vector2 spawnPosition = new Vector2(xSpawnAreaPosition, ySpawnAreaPosition) + currentSwarm.spawnAreaCenter + relativeCasterPosition;

                Object.Instantiate(spawnCreature, spawnPosition, Quaternion.identity);

                newTimes.Add(currentSwarm, currentTime);
            }
            swarmSpawnTimes = newTimes;

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(postCastDelay);

        BreakCast();
    }
}
