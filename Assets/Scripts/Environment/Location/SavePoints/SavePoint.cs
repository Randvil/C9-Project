using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class SavePoint : MonoBehaviour
{
    private GameData gameData = new();
    private FileDataHandler dataHandler;
    private List<IDataSavable> savableObjects;

    protected virtual void Start()
    {
        dataHandler = new FileDataHandler("Saves", "LastSave");
    }

    public void SaveGame(GameData gameData)
    {
        dataHandler.Save(gameData);
    }

    public List<IDataSavable> FindAllSavableObjects()
    {
        IEnumerable<IDataSavable> dataSavableObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataSavable>();

        return new List<IDataSavable>(dataSavableObjects);
    }

    public GameData LoadDataFromFile()
    {
        return dataHandler.Load();
    }

    public abstract GameData GetData(List<IDataSavable> savableObjects);
}
