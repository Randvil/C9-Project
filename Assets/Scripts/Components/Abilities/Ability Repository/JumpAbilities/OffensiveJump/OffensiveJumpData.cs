using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOffensiveJumpData", menuName = "Data/Abilities/Offensive Jump/New Offensive Jump Data")]
public class OffensiveJumpData : DefensiveJumpData
{
    [Header("Offensive Jump Data")]
    public DamageData damageData;
    public LayerMask enemyLayers;
}
