using System.Collections.Generic;
using System.Collections;
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

    private PanelManager panelManager;
    private LoadScreen loadScreen;

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

    protected override void Start()
    {
        base.Start();

        panelManager = FindObjectOfType<PanelManager>(); // creates from GameLoader
        loadScreen = panelManager.GetComponentInChildren<LoadScreen>();
    }

    public void StartInteraction(GameObject interactingCharacter)
    {
        loadScreen.EndOfScene();

        HideTooltip();

        IsInteracting = true;

        SaveGame(GetData(FindAllSavableObjects()));

        StartCoroutine(LoadSceneCoroutine());
    }

    private IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSecondsRealtime(panelManager.PanelTweenDuration);
        SceneManager.LoadScene(sceneName);
    }

    public void StopInteraction()
    {

    }

    public void NextStep()
    {

    }
}
