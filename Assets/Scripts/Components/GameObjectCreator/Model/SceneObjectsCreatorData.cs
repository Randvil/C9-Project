using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scene Objects Creator Data", menuName = "Component Data/Model/New Scene Objects Creator Data", order = 170)]
public class SceneObjectsCreatorData : ScriptableObject
{
    public GameObject playerPrefab;
    public GameObject spawnpointPrefab;
    public GameObject cameraPrefab;
    public GameObject managersPrefab;
    public GameObject staticUIPrefab;
}
