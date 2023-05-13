using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCreatureSpawnerData", menuName = "Data/Abilities/Creatur Spawner/New Creature Spawner Data")]
public class CreatureSpawnerData : BaseAbilityData
{
    [Header("Creature Spawner Data")]
    public GameObject creaturePrefab;
    [Min(1)]
    public int creatureCount;
    [Min(0.01f), Tooltip("Delay between spawning multiple creatures")]
    public float spawnDelay;
}
