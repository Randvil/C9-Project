using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class DeathLoad: IDeathLoad
{
    public DeathLoad() { }

    public DeathLoad(IDeathManager deathManager)
    {
        deathManager.DeathEvent.AddListener(RewriteData);
    }

    public void RewriteData()
    {
        FileDataHandler dataHandler = new FileDataHandler("Saves", "LastSave");
        GameData gameData = dataHandler.Load();
        gameData.CurrentGameData = gameData.CheckpointData;
        dataHandler.Save(gameData);
    }

    public void LoadCheckpoint()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        FileDataHandler dataHandler = new FileDataHandler("Saves", "LastSave");
        GameData gameData = dataHandler.Load();

        switch (gameData.CheckpointData.latestScene)
        {
            case eSceneName.CityLocation:
                SceneManager.LoadScene("CityLocation");
                break;
            case eSceneName.ArcadeCenter:
                SceneManager.LoadScene("ArcadeCenter");
                break;
            case eSceneName.BossLocation:
                SceneManager.LoadScene("BossLocation");
                break;
        }
    }
}
