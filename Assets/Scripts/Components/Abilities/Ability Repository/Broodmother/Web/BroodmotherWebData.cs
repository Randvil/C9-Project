using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBroodmotherWebData", menuName = "Data/Abilities/New Broodmother Web Data")]
public class BroodmotherWebData : DamageAbilityData
{
    [Header("Broodmother Web")]
    public GameObject projectilePrefab;
    [Min(1)]
    public int webSeries = 1;
    [Min(0.01f), Tooltip("Delay between webs in a series")]
    public float seriesDelay = 0.5f;
}
