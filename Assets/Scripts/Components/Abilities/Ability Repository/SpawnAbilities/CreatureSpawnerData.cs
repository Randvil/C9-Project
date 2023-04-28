using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Creature Spawner Data", menuName = "Component Data/Model/New Creature Spawner Data", order = 315)]
public class CreatureSpawnerData : BaseAbilityData
{
    [Header("Creature Spawner Data")]
    public GameObject creaturePrefab;
    public int creatureCount;
    public float spawnDelay;
}
