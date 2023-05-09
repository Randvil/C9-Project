using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnEnemyCondition : ISpawnEnemyCondition
{
    public int spawnEnemyCount;

    public abstract void Spawn();

    public int SpawnEnemyCount()
    {
        return spawnEnemyCount;
    }

    public void SpawnOneEnemy(GameObject enemyPrefab, Spawnpoint spawnpoint, Material material)
    {
        GameObject instantiate = Object.Instantiate(enemyPrefab, GenerateRandomPos(spawnpoint.transform.position), 
            Quaternion.identity, spawnpoint.transform);
        //Material materialClone = Object.Instantiate<Material>(material);
        //instantiate.GetComponentInChildren<SkinnedMeshRenderer>().material = materialClone;
        instantiate.name = enemyPrefab.name;
        spawnEnemyCount++;
    }
    public Vector3 GenerateRandomPos(Vector3 pos)
    {
        return new Vector3(Random.Range(pos.x - 2f, pos.x + 2f), pos.y, pos.z);
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
                if (o.TryGetComponent(out ITeamMember teamMember) && teamMember.CharacterTeam.Team == eTeam.Player)
                {
                    return true;
                }
                       
            }
        }
        return false;
    }
}
