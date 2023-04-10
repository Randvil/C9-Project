using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creator
{
    public GameObject newGameObject;
    public GameObject CreateObject(GameObject prefab, GameData data)
    {
        newGameObject = Object.Instantiate(prefab);
        newGameObject.name = prefab.name;
        return newGameObject;
    }

    public virtual void LoadDataToObject(GameData data) { }

    public virtual void CreateAllObjects(GameData data) { }
}
