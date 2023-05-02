using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AcidDropData", menuName = "Data/Acid Ceiling/New Acid Drop Data")]
public class AcidDropData : ScriptableObject
{
    [Tooltip("Splash damage")]
    public DamageData explosionDamageData;
    [Min(0f), Tooltip("When the drop touches something, it deals area damage of the specified radius [units]")]
    public float explosionRadius = 1f;
    public GameObject acidPuddlePrefab;
}
