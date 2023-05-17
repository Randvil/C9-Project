using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObjectsCreator : Creator
{
    private SceneObjectsCreatorData prefabsData;
    private string currentSceneName;

    public SceneObjectsCreator(SceneObjectsCreatorData prefabsData)
    {
        this.prefabsData = prefabsData;
    }

    public override void CreateAllObjects(GameData data)
    {
        CameraCreator camera = new CameraCreator();
        camera.CreateObject(prefabsData.cameraPrefab, data);

        ManagersCreator managers = new ManagersCreator();
        managers.CreateObject(prefabsData.managersPrefab, data);

        StaticUICreator staticUI = new StaticUICreator();
        staticUI.CreateObject(prefabsData.staticUIPrefab, data);

        SceneEffectsCreator sceneEffects = new SceneEffectsCreator();
        sceneEffects.CreateObject(prefabsData.postProcessingEffects, data);

        PlayerCreator player = new PlayerCreator();
        player.CreateObject(prefabsData.playerPrefab, data);

        player.PlayerComponent.volume = sceneEffects.Volume;
        player.PlayerComponent.Initialize(managers.PlayerInput);
        player.PlayerComponent.Document = staticUI.UIDocument;
        player.LoadDataToObject(data);
        
        staticUI.PanelManager.Input = managers.PlayerInput;
        staticUI.PanelManager.Abilities = player.PlayerComponent.AbilityManager;
        staticUI.newGameObject.GetComponentInChildren<DeathScreen>().SetDeathManager(player.PlayerComponent.DeathManager);
        staticUI.newGameObject.GetComponentInChildren<AbilityUiDependencies>().Parry = player.PlayerComponent.Parry;


        var targetGroup = camera.newGameObject.GetComponentInChildren<CinemachineTargetGroup>();
        targetGroup.AddMember(player.PlayerComponent.transform, 17f, 0f);
        
        GameObject boundingShape = null;
        switch (SceneManager.GetActiveScene().name)
        {
            case "CityLocation":
                boundingShape = Object.Instantiate(prefabsData.cityBoundingShape);
                break;

            case "ArcadeCenter":
                boundingShape = Object.Instantiate(prefabsData.arcadeBoundingShape);
                break;

            case "BossLocation":
                boundingShape = Object.Instantiate(prefabsData.bossBoundingShape);
                targetGroup.AddMember(GameObject.Find("Broodmother").transform, 10f, 0f);
                break;
        }
        var cameraConfiner = camera.CinemachineCamera.GetComponent<CinemachineConfiner>();
        cameraConfiner.m_BoundingShape2D = boundingShape.GetComponent<Collider2D>();


        foreach (LocationData ld in data.CurrentGameData.locations)
        {
            switch (ld.sceneName)
            {
                case eSceneName.CityLocation:
                    currentSceneName = "CityLocation";
                    break;
                case eSceneName.ArcadeCenter:
                    currentSceneName = "ArcadeCenter";
                    break;
                case eSceneName.BossLocation:
                    currentSceneName = "BossLocation";
                    break;
            }

            if (currentSceneName == SceneManager.GetActiveScene().name)
            {
                foreach (SpawnpointsOnce once in ld.SpawnpointsOnce)
                {
                    if (once.enemyNumber > 0)
                    {
                        SpawnpointCreator spawnpoint = new SpawnpointCreator();
                        spawnpoint.CreateObject(prefabsData.spawnpointPrefab, data);
                        spawnpoint.SpawnpointComponent.SetSpawnpointInfo(once.id, once.position, once.enemyPrefab, once.enemyMaterial,
                            once.spawnCondition);
                        spawnpoint.SpawnpointComponent.SetSpawnpointCondition(once.spawnOnceData.detectPlayerRadius,
                            once.spawnOnceData.spawnRadius);
                    }
                }

                foreach (SpawnpointsWave wave in ld.SpawnpointsWave)
                {
                    if (wave.spawnWaveData.enemyPerWaveCount > 0)
                    {
                        SpawnpointCreator spawnpoint = new SpawnpointCreator();
                        spawnpoint.CreateObject(prefabsData.spawnpointPrefab, data);
                        spawnpoint.SpawnpointComponent.SetSpawnpointInfo(wave.id, wave.position, wave.enemyPrefab, wave.enemyMaterial,
                            wave.spawnCondition);
                        spawnpoint.SpawnpointComponent.SetSpawnpointCondition(wave.spawnWaveData.detectPlayerRadius,
                            wave.spawnWaveData.spawnRadius, wave.spawnWaveData.waveCount, wave.spawnWaveData.enemyPerWaveCount,
                            wave.spawnWaveData.timeBetweenWaves);
                    }
                }

            }
        }
    }
}
