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

        dataHandler = new FileDataHandler("Saves", "LastSave");
        gameData = dataHandler.Load();

        creator = new SceneObjectsCreator(prefabsData);
        creator.CreateAllObjects(gameData);
    }

}
