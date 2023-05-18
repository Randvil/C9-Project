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
    public GameObject cityBoundingShape;
    public GameObject arcadeBoundingShape;
    public GameObject bossBoundingShape;
    public List<AbilityPickUpStruct> abilityPickUpPrefabs;
}

[System.Serializable]
public struct AbilityPickUpStruct
{
    public GameObject prefab;
    public eAbilityType abilityType;
    public Vector3 position;
    public eSceneName scene;
}

