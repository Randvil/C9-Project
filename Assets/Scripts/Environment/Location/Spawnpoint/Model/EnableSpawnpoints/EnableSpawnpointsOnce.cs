using System.Collections;
using System.Linq;
using UnityEngine;

public class EnableSpawnpointsOnce : MonoBehaviour
{
    private FileDataHandler dataHandler;
    private GameData gameData = new();
    void Start()
    {
        dataHandler = new FileDataHandler("Saves", "LastSave");
        gameData = dataHandler.Load();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnableSpawnpoints();
        gameObject.SetActive(false);
    }

    public void EnableSpawnpoints()
    {
        foreach (LocationData locationData in gameData.CurrentGameData.locations)
        {
            switch (locationData.sceneName)
            {
                case eSceneName.ArcadeCenter:
                    // enables spawnpoints in ArcadeCenter
                    foreach (SpawnpointsOnce spawnpoint in locationData.SpawnpointsOnce)
                    {
                        if (spawnpoint.enemyNumber == -1)
                            spawnpoint.enemyNumber = 1;
                    }
                    break;
                case eSceneName.CityLocation:
                    // enables spawnpoints in CityLocation
                    foreach (SpawnpointsOnce spawnpoint in locationData.SpawnpointsOnce)
                    {
                        if (spawnpoint.enemyNumber == -1)
                            spawnpoint.enemyNumber = 1;
                    }
                    break;
            }
        }

        dataHandler.Save(gameData);
    }
}
