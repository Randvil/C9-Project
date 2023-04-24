using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnEnemyCondition : ISpawnEnemyCondition
{
    private ITeamMember team;
    public int spawnEnemyCount;

    public abstract void Spawn();

    public int SpawnEnemyCount()
    {
        return spawnEnemyCount;
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
                o.TryGetComponent(out team);
                if (team != null && team.CharacterTeam.Team == eTeam.Player)
                {
                    return true;
                }
                       
            }
        }
        return false;
    }
}
