using UnityEngine.SceneManagement;

public class DeathLoad
{
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
        FileDataHandler dataHandler = new FileDataHandler("Saves", "LastSave");
        GameData gameData = dataHandler.Load();
        SceneManager.LoadScene(gameData.CheckpointData.scene);
    }
}
