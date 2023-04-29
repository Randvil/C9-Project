using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class LocationEntry : SavePoint, IInteractive
{
    GameData gameData = new GameData();

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private string tip;

    [SerializeField]
    private Vector3 nextScenePosition;

    public bool IsInteracting { get; private set; }

    public override GameData GetData(List<IDataSavable> savableObjects)
    {
        gameData = LoadDataFromFile();
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
        gameData.CurrentGameData.position = nextScenePosition;
        return gameData;
    }

    public void ShowTooltip()
    {

    }

    public void HideTooltip()
    {
        
    }

    public void StartInteraction()
    {
        HideTooltip();

        IsInteracting = true;

        SaveGame();

        SceneManager.LoadScene(sceneName);
    }

    public void StopInteraction()
    {

    }

    public void NextStep()
    {

    }
}
