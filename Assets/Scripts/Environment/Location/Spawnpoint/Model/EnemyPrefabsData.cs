using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Prefabs Data", menuName = "Spawnpoint Data/Model/Enemy Prefabs Data", order = 170)]
public class EnemyPrefabsData : ScriptableObject
{
    public GameObject spiderPrefab;
    public GameObject spiderBoyPrefab;
    public GameObject flyingEyePrefab;
    public GameObject bossPrefab;

    public Material dissolveSpiderBoyMaterial;
    public Material dissolveSpiderMaterial;
    public Material dissolveFlyingEyeMaterial;
    public Material dissolveBossMaterial;
}
