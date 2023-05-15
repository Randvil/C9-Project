using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AreaTessenData", menuName = "Data/Abilities/Area Tessen/New Area Tessen Data")]
public class AreaTessenData : BaseAbilityData
{
    [Header("Area Tessen Data")]
    public GameObject ascensionAreaPrefab;
    [Min(0f)]
    public float areaSpawnDistance = 5f;
}
