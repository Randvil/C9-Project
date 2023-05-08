using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSceneObjectsCreatorData", menuName = "Data/Scene Objects/New Scene Objects Creator Data")]
public class SceneObjectsCreatorData : ScriptableObject
{
    public GameObject playerPrefab;
    public GameObject spawnpointPrefab;
    public GameObject cameraPrefab;
    public GameObject managersPrefab;
    public GameObject staticUIPrefab;
    public GameObject postProcessingEffects;
}
