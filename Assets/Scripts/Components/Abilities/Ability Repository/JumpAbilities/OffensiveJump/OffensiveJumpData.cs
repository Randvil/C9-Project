using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Offensive Jump Data", menuName = "Component Data/Model/New Offensive Jump Data", order = 312)]
public class OffensiveJumpData : DefensiveJumpData
{
    public DamageData damageData;
    public LayerMask enemyLayers;
}
