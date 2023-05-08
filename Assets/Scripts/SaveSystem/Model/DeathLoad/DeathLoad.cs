using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class DeathLoad: IDeathLoad
{
    public DeathLoad() { }

    public DeathLoad(IDeathManager deathManager)
    {
        deathManager.DeathEvent.AddListener(RewriteData);
        deathManager.DeathEvent.AddListener(LoadCheckpoint);
    }

    public void RewriteData()
    {
        FileDataHandler dataHandler = new FileDataHandler("Saves", "LastSave");
        GameData gameData = dataHandler.Load();
        gameData.CurrentGameData.playerHealth = gameData.CheckpointData.playerHealth;
        gameData.CurrentGameData.position = gameData.CheckpointData.position;
        gameData.CurrentGameData.locations = gameData.CheckpointData.locations;
        dataHandler.Save(gameData);
    }

    public void LoadCheckpoint()
    {
        LoadSceneCoroutine();
    }

    public void LoadSceneCoroutine()
    {
        FileDataHandler dataHandler = new FileDataHandler("Saves", "LastSave");
        GameData gameData = dataHandler.Load();
        //yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(gameData.CheckpointData.latestScene);
    }
}
