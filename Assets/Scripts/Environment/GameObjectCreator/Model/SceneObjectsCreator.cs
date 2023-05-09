using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObjectsCreator : Creator
{
    private SceneObjectsCreatorData prefabsData;

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

        camera.CinemachineCamera.Follow = player.PlayerComponent.CameraFollowPoint;

        foreach (LocationData ld in data.CurrentGameData.locations)
        {
            if (ld.sceneName == SceneManager.GetActiveScene().name)
            {
                foreach (SpawnpointData spdata in ld.spawnpoints)
                {
                    if (spdata.enemyNumber != 0)
                    {
                        SpawnpointCreator spawnpoint = new SpawnpointCreator();
                        spawnpoint.CreateObject(prefabsData.spawnpointPrefab, data);
                        spawnpoint.SpawnpointComponent.StartSpawnpoint(spdata.id, spdata.position, spdata.enemyNumber, spdata.timeCountdown,
                            spdata.enemyPrefabName, spdata.spawnRadius, spdata.condition, spdata.waveCount);
                    }
                }
            }
        }
    }
}