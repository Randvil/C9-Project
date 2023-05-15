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

    private List<SpawnpointsOnce> CitySpawnpointsOnce;
    private List<SpawnpointsWave> CitySpawnpointsWave;
    private List<SpawnpointsOnce> ArcadeCenterOnce;
    private List<SpawnpointsWave> ArcadeCenterWave;

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
                case eSceneName.CityLocation:
                    CitySpawnpointsOnce = locationData.SpawnpointsOnce;
                    CitySpawnpointsWave = locationData.SpawnpointsWave;
                    break;
                case eSceneName.ArcadeCenter:
                    ArcadeCenterOnce = locationData.SpawnpointsOnce;
                    ArcadeCenterWave = locationData.SpawnpointsWave;
                    break;
            }
        }

        if (spawnpointsID.CityLocation.Count != 0)
        {
            foreach (int id in spawnpointsID.CityLocation)
            {
                SpawnpointsOnce once = CitySpawnpointsOnce.Find(spawnpoint => spawnpoint.id == id);
                if (once != null && once.enemyNumber > 0)
                {
                    return true;
                }     
            }

            foreach (int id in spawnpointsID.CityLocation)
            {
                SpawnpointsWave wave = CitySpawnpointsWave.Find(spawnpoint => spawnpoint.id == id);
                if (wave != null && wave.spawnWaveData.enemyPerWaveCount != 0)
                {
                    return true;
                }
            }
        }

        if (spawnpointsID.ArcadeCenter.Count != 0)
        {
            foreach (int id in spawnpointsID.ArcadeCenter)
            {
                SpawnpointsOnce once = ArcadeCenterOnce.Find(spawnpoint => spawnpoint.id == id);
                if (once != null && once.enemyNumber > 0)
                {
                    return true;
                }
            }

            foreach (int id in spawnpointsID.ArcadeCenter)
            {
                SpawnpointsWave wave = ArcadeCenterWave.Find(spawnpoint => spawnpoint.id == id);
                if (wave != null && wave.spawnWaveData.enemyPerWaveCount > 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool EntranceOpen()
    {
        if (!AnyEnemiesAlive())
            EntranceOpenEvent.Invoke();
        return !AnyEnemiesAlive();
    }
}
