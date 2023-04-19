using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class EnemyCheckEntrance : MonoBehaviour, ICheckEntrance
{
    [SerializeField]
    private SpawnpointsCleanUpData spawnpointsID;

    private FileDataHandler dataHandler;
    private GameData gameData = new();
    private List<SpawnpointData> CitySpawnpoints;
    private List<SpawnpointData> ArcadeCenterSpawnpoints;

    public UnityEvent EntranceOpenEvent { get; } = new();

    public void Awake()
    {
        dataHandler = new FileDataHandler("Saves", "LastSave");
        gameData = dataHandler.Load();
    }

    public bool AnyEnemiesAlive()
    {
        IEnumerable<Spawnpoint> spawnpoints = FindObjectsOfType<MonoBehaviour>().OfType<Spawnpoint>();

        foreach (Spawnpoint spawnpoint in spawnpoints)
        {
            spawnpoint.SaveData(gameData.CurrentGameData);
        }

        foreach (LocationData locationData in gameData.CurrentGameData.locations)
        {

            switch (locationData.sceneName)
            {
                case "CityLocation":
                    CitySpawnpoints = locationData.spawnpoints;
                    break;
                case "ArcadeCenter":
                    ArcadeCenterSpawnpoints = locationData.spawnpoints;
                    break;
            }
        }

        if (spawnpointsID.CityLocation.Count != 0)
        {
            foreach (int id in spawnpointsID.CityLocation)
            {
                SpawnpointData sp = CitySpawnpoints.Find(spawnpoint => spawnpoint.id == id);
                if (sp.enemyNumber > 0)
                    return true;
            }
        }

        if (spawnpointsID.ArcadeCenter.Count != 0)
        {
            foreach (int id in spawnpointsID.ArcadeCenter)
            {
                SpawnpointData sp = ArcadeCenterSpawnpoints.Find(spawnpoint => spawnpoint.id == id);
                if (sp.enemyNumber > 0)
                    return true;
            }
        }

        return false;
    }

    public bool EntranceOpen()
    {
        return !AnyEnemiesAlive();
    }
}
