using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class Checkpoint : SavePoint
{
    GameData gameData = new();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        SaveGame();
        gameObject.SetActive(false);
    }

    public override GameData GetData(List<IDataSavable> savableObjects)
    {
        gameData = LoadDataFromFile();
        try
        {
            foreach (IDataSavable savableObject in savableObjects)
            {
                savableObject.SaveData(gameData.CheckpointData);
            }

            switch (SceneManager.GetActiveScene().name)
            {
                case "CityLocation" :
                    gameData.CheckpointData.latestScene = eSceneName.CityLocation;
                    break;
                case "ArcadeCenter":
                    gameData.CheckpointData.latestScene = eSceneName.ArcadeCenter;
                    break;
                case "BossLocation":
                    gameData.CheckpointData.latestScene = eSceneName.BossLocation;
                    break;
            }

        }
        catch (Exception e) {
            Debug.LogError("Couldn't save \n" + e);
        }
        return gameData;
    }
}
