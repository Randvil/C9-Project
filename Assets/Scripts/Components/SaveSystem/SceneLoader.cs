using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    private GameData gameData;
    private FileDataHandler dataHandler;
    private Creator player = new PlayerCreator();
    private Creator spawnpoint = new SpawnpointCreator();

    private void Start()
    {
        dataHandler = new FileDataHandler("Saves", "LastSave");
        gameData = dataHandler.Load();

        player.CreateObject("Player", gameData);
        spawnpoint.CreateAllObjects(gameData);
    }

}
