using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LocationEntry : SavePoint
{
    GameData gameData = new GameData();

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private Vector3 nextScenePosition;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        SaveGame();
        Debug.Log("to arcade center");
        SceneManager.LoadScene(sceneName);
    }

    public override GameData GetData(List<IDataSavable> savableObjects)
    {
        gameData = LoadDataFromFile();
        gameData.CurrentGameData.position = nextScenePosition;
        try
        {
            foreach (IDataSavable savableObject in savableObjects)
            {
                savableObject.SaveData(gameData.CurrentGameData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Couldn't save \n" + e);
        }
        return gameData;
    }

    

}
