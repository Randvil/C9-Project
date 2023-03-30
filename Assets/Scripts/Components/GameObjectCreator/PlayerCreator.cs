using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCreator : Creator
{
    public override void LoadDataToObject(GameData data)
    {
        Creator camera = new CameraCreator();
        camera.CreateObject("PlayerCamera", data);
        CameraController cameraController = camera.newGameObject.GetComponent<CameraController>();

        Creator managers = new ManagersCreator();
        managers.CreateObject("Managers", data);

        Player player = newGameObject.GetComponent<Player>();
        player.unityPlayerInput = managers.newGameObject.GetComponent<PlayerInput>();
        player.Initialize();
        player.gameObject.transform.position = data.CheckpointData.position;
        player.HealthManager.ChangeCurrentHealth(- (player.HealthManager.Health.currentHealth - data.CheckpointData.playerHealth));

        cameraController.player = player.gameObject.transform;
    }
}
