using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class Checkpoint : SavePoint
{
    GameData gameData = new();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        SaveGame(GetData(FindAllSavableObjects()));
        gameObject.SetActive(false);
    }

    public override GameData GetData(List<IDataSavable> savableObjects)
    {
        gameData = LoadDataFromFile();
        gameData.CheckpointData.locations = gameData.CurrentGameData.locations;
        try
        {
            foreach (IDataSavable savableObject in savableObjects)
            {
                savableObject.SaveData(gameData.CheckpointData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Couldn't save \n" + e);
        }

        switch (SceneManager.GetActiveScene().name)
        {
            case "CityLocation":
                gameData.CheckpointData.latestScene = eSceneName.CityLocation;
                break;
            case "ArcadeCenter":
                gameData.CheckpointData.latestScene = eSceneName.ArcadeCenter;
                break;
            case "BossLocation":
                gameData.CheckpointData.latestScene = eSceneName.BossLocation;
                break;
        }

        return gameData;
    }
}
