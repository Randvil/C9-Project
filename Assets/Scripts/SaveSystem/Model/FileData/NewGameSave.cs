using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameSave : MonoBehaviour
{
    [SerializeField]
    private NewGameData newGameData;

    public GameData gameData;

    public void CreateNewGameSave()
    {
        FileDataHandler dataHandler = new FileDataHandler("Saves", "LastSave");
        gameData = new GameData(newGameData.CheckpointData, newGameData.CheckpointData);
        dataHandler.Save(gameData);
    }
}
