using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creator
{
    public GameObject newGameObject;
    public GameObject CreateObject(string prefabName, GameData data)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        newGameObject = Object.Instantiate(prefab);
        newGameObject.name = prefabName;
        LoadDataToObject(data);
        return newGameObject;
    }

    public virtual void LoadDataToObject(GameData data) { }

    public virtual void CreateAllObjects(GameData data) { }
}
