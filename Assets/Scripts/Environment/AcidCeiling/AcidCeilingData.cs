using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AcidCeilingData", menuName = "Data/Acid Ceiling/New Acid Ceiling Data")]
public class AcidCeilingData : ScriptableObject
{
    public GameObject dropPrefab;
    [Tooltip("Ceiling doesn't affect the members of this team")]
    public eTeam ceilingTeam = eTeam.Enemies;
    [Min(0f), Tooltip("How often drops will spawn [seconds]")]
    public float createDropPeriod = 0.5f;
}
