using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCreator : Creator
{
    public override void LoadDataToObject(GameData data)
    {
        Creator camera = new CameraCreator();
        camera.CreateObject("PlayerCamera", data);
        CameraController cameraController = camera.newGameObject.GetComponent<CameraController>();

        Creator managers = new ManagersCreator();
        managers.CreateObject("Managers", data);

        Creator staticUI = new ManagersCreator();
        staticUI.CreateObject("StaticUI", data);

        Player player = newGameObject.GetComponent<Player>();
        staticUI.newGameObject.GetComponent<PanelManager>().Input = player.unityPlayerInput = managers.newGameObject.GetComponent<PlayerInput>();
        player.Initialize();
        player.Document = staticUI.newGameObject.GetComponentInChildren<UIDocument>();
        player.gameObject.transform.position = data.CheckpointData.position;
        player.HealthManager.ChangeCurrentHealth(- (player.HealthManager.Health.currentHealth - data.CheckpointData.playerHealth));

        staticUI.newGameObject.GetComponent<PanelManager>().Abilities = player.AbilityManager;

        cameraController.player = player.gameObject.transform;
    }
}
