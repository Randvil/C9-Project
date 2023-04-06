using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCreator : Creator
{
    public Player PlayerComponent { get => newGameObject.GetComponent<Player>(); }

    public override void LoadDataToObject(GameData data)
    {
        newGameObject.transform.position = data.CurrentGameData.position;
        newGameObject.GetComponent<Player>().HealthManager.ChangeCurrentHealth(-(PlayerComponent.HealthManager.Health.currentHealth - data.CurrentGameData.playerHealth));
    }

}
