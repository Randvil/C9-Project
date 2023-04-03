using UnityEngine.SceneManagement;

public class SpawnpointCreator : Creator
{
    public override void CreateAllObjects(GameData data)
    {
        foreach (LocationData ld in data.CurrentGameData.locations)
        {
            if (ld.sceneName == SceneManager.GetActiveScene().name)
            {
                foreach (SpawnpointData spdata in ld.spawnpoints)
                {
                    if (spdata.enemyNumber != 0)
                    {
                        CreateObject("Spawnpoint", data);
                        Spawnpoint sp = newGameObject.GetComponent<Spawnpoint>();
                        sp.StartSpawnpoint(spdata.id, spdata.position, spdata.enemyNumber, spdata.killTime, spdata.enemyPrefabName,
                            spdata.spawnRadius, spdata.condition);

                    }
                }
            }
        }
    }
}
