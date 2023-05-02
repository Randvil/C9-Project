using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSwarmSpawningData", menuName = "Data/Abilities/New Swarm Spawning Data")]
public class SwarmSpawningData : BaseAbilityData
{
    [Header("Swarm Spawning Data")]
    [Min(0f)]
    public float spawnDuration;
    public Swarm[] swarms;
}
