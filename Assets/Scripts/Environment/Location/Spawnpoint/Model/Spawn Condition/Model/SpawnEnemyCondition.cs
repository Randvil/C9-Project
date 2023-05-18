using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnEnemyCondition : ISpawnEnemyCondition
{
    public int spawnEnemyCount;

    public abstract void Spawn();
    public abstract ISpawnEnemiesConditionData ReturnInfo();
    public int SpawnEnemyCount()
    {
        return spawnEnemyCount;
    }

    public void SpawnOneEnemy(GameObject enemyPrefab, Spawnpoint spawnpoint, Material material, float diff)
    {
        GameObject instantiate = Object.Instantiate(enemyPrefab, GenerateRandomPos(spawnpoint.transform.position, diff), 
            Quaternion.identity, spawnpoint.transform);
        //Material materialClone = Object.Instantiate(material);
        //instantiate.GetComponentInChildren<SkinnedMeshRenderer>().material = materialClone;
        instantiate.name = enemyPrefab.name;
        spawnEnemyCount++;
    }
    public Vector3 GenerateRandomPos(Vector3 pos, float diff)
    {
        return new Vector3(Random.Range(pos.x - diff, pos.x + diff), pos.y, pos.z);
    }

    public bool InRadius(Spawnpoint spawnpoint, float spawnRadius)
    {
        Collider2D[] objectsNear = Physics2D.OverlapCircleAll(spawnpoint.transform.position, spawnRadius);

        if (objectsNear.Length == 0)
            return false;
        else
        {
            foreach (Collider2D o in objectsNear)
            {
                if (o.TryGetComponent(out ITeamMember teamMember) && teamMember.CharacterTeam != null
                        && teamMember.CharacterTeam.Team == eTeam.Player)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
