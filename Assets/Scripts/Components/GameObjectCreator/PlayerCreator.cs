using UnityEngine;

public class PlayerCreator : Creator
{
    public override void LoadDataToObject(GameData data)
    {
        Player player = newGameObject.GetComponent<Player>();
        player.gameObject.transform.position = data.CheckpointData.position;
        player.gameObject.GetComponent<IStats>().SetStat(eStatType.CurrentHealth, data.CheckpointData.playerHealth);

        Creator camera = new CameraCreator();
        camera.CreateObject("PlayerCamera", data);
        CameraController cameraController = camera.newGameObject.GetComponent<CameraController>();
        cameraController.player = player.gameObject.transform;
    }
}
