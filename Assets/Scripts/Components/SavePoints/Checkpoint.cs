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
        Debug.Log("save on " + name);
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

            gameData.CheckpointData.scene = SceneManager.GetActiveScene().name;
        }
        catch (Exception e) {
            Debug.LogError("Couldn't save \n" + e);
        }
        return gameData;
    }
}
