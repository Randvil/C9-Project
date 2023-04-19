using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    private GameData gameData;
    private FileDataHandler dataHandler;
    private Creator creator;

    [SerializeField]
    private SceneObjectsCreatorData prefabsData;

    private void Awake()
    {
        //only for testing
        //remove later
        NewGameSave newGame = GetComponent<NewGameSave>();
        newGame.CreateNewGameSave();


        dataHandler = new FileDataHandler("Saves", "LastSave");
        gameData = dataHandler.Load();

        creator = new SceneObjectsCreator(prefabsData);
        creator.CreateAllObjects(gameData);
    }

}
