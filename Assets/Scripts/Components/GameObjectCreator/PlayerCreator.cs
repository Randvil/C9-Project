using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCreator : Creator
{
    public override void LoadDataToObject(GameData data)
    {
        Creator camera = new CameraCreator();
        camera.CreateObject("PlayerCamera", data);
        CinemachineVirtualCamera cinemacineCamera = camera.newGameObject.GetComponentInChildren<CinemachineVirtualCamera>();

        Creator managers = new ManagersCreator();
        managers.CreateObject("Managers", data);

        Creator staticUI = new ManagersCreator();
        staticUI.CreateObject("StaticUI", data);

        Player player = newGameObject.GetComponent<Player>();
        PlayerInput playerInput = managers.newGameObject.GetComponent<PlayerInput>();
        staticUI.newGameObject.GetComponent<PanelManager>().Input = playerInput;
        player.Initialize(playerInput);
        player.Document = staticUI.newGameObject.GetComponentInChildren<UIDocument>();
        player.gameObject.transform.position = data.CurrentGameData.position;
        player.HealthManager.ChangeCurrentHealth(-(player.HealthManager.Health.currentHealth - data.CurrentGameData.playerHealth));

        staticUI.newGameObject.GetComponent<PanelManager>().Abilities = player.AbilityManager;

        cinemacineCamera.Follow = player.CameraFollowPoint;
    }
}
