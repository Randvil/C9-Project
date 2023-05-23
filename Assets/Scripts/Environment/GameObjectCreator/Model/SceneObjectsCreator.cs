using Cinemachine;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneObjectsCreator : Creator
{
    private SceneObjectsCreatorData prefabsData;
    private eSceneName currentScene;

    public SceneObjectsCreator(SceneObjectsCreatorData prefabsData)
    {
        this.prefabsData = prefabsData;
    }

    public override void CreateAllObjects(GameData data)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "CityLocation":
                currentScene = eSceneName.CityLocation;
                break;
            case "ArcadeCenter":
                currentScene = eSceneName.ArcadeCenter;
                break;
            case "BossLocation":
                currentScene = eSceneName.BossLocation;
                break;
        }

        CameraCreator camera = new();
        camera.CreateObject(prefabsData.cameraPrefab, data);

        ManagersCreator managers = new();
        managers.CreateObject(prefabsData.managersPrefab, data);

        StaticUICreator staticUI = new();
        staticUI.CreateObject(prefabsData.staticUIPrefab, data);

        SceneEffectsCreator sceneEffects = new();
        sceneEffects.CreateObject(prefabsData.postProcessingEffects, data);

        PlayerCreator player = new();
        player.CreateObject(prefabsData.playerPrefab, data);

        player.PlayerComponent.volume = sceneEffects.Volume;
        player.PlayerComponent.Initialize(managers.PlayerInput);
        player.PlayerComponent.Document = staticUI.UIDocument;
        player.LoadDataToObject(data);
        
        staticUI.PanelManager.Input = managers.PlayerInput;
        staticUI.PanelManager.Abilities = player.PlayerComponent.AbilityManager;
        staticUI.newGameObject.GetComponentInChildren<DeathScreen>().SetDeathManager(player.PlayerComponent.DeathManager);
        staticUI.newGameObject.GetComponentInChildren<AbilityUiDependencies>().Parry = player.PlayerComponent.Parry;
        staticUI.newGameObject.GetComponentInChildren<EffectUiDependencies>().EffectManager = player.PlayerComponent.EffectManager;


        var targetGroup = camera.newGameObject.GetComponentInChildren<CinemachineTargetGroup>();
        targetGroup.AddMember(player.PlayerComponent.transform, 17f, 0f);
        
        GameObject boundingShape = null;

        switch (SceneManager.GetActiveScene().name)
        {
            case "CityLocation":
                camera.CinemachineCamera.Follow = player.PlayerComponent.CameraFollowPoint;
                boundingShape = Object.Instantiate(prefabsData.cityBoundingShape);
                break;

            case "ArcadeCenter":
                camera.CinemachineCamera.Follow = player.PlayerComponent.CameraFollowPoint;
                boundingShape = Object.Instantiate(prefabsData.arcadeBoundingShape);
                camera.CinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 0.2f;
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

            if (currentScene == ld.sceneName)
            {
                foreach (SpawnpointsOnce once in ld.SpawnpointsOnce)
                {
                    SpawnpointCreator spawnpoint = new();
                    spawnpoint.CreateObject(prefabsData.spawnpointPrefab, data);
                    spawnpoint.SpawnpointComponent.SetSpawnpointInfo(once.id, once.position, once.enemyPrefab, once.enemyMaterial,
                        once.spawnCondition);
                    spawnpoint.SpawnpointComponent.SetSpawnpointCondition(once.spawnOnceData.detectPlayerRadius,
                        once.spawnOnceData.spawnRadius, once.enemyNumber);
                }

                foreach (SpawnpointsWave wave in ld.SpawnpointsWave)
                {
                    SpawnpointCreator spawnpoint = new();
                    spawnpoint.CreateObject(prefabsData.spawnpointPrefab, data);
                    spawnpoint.SpawnpointComponent.SetSpawnpointInfo(wave.id, wave.position, wave.enemyPrefab, wave.enemyMaterial,
                        wave.spawnCondition);
                    spawnpoint.SpawnpointComponent.SetSpawnpointCondition(wave.spawnWaveData.detectPlayerRadius,
                        wave.spawnWaveData.spawnRadius, wave.spawnWaveData.waveCount, wave.spawnWaveData.enemyPerWaveCount,
                        wave.spawnWaveData.timeBetweenWaves);
                }

            }
        }

        Hints hintView = staticUI.newGameObject.GetComponentInChildren<Hints>();
        foreach (AbilityPickUpStruct abilityPickUp in prefabsData.abilityPickUpPrefabs)
        {
            if (data.CurrentGameData.learnedAbilities.FirstOrDefault(x => x.abilityType == abilityPickUp.abilityType) == null
                && currentScene == abilityPickUp.scene)
            {
                AbilityPickUpCreator abilityPickUpCreator = new();
                abilityPickUpCreator.CreateObject(abilityPickUp.prefab, data);
                abilityPickUpCreator.newGameObject.transform.position = abilityPickUp.position;
                hintView.AddHintable(abilityPickUpCreator.newGameObject.GetComponent<Hintable>());
            }
        }
    }
}
