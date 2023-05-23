using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageHandlerWithShieldsData", menuName = "Data/Components/Damage Handler/New Damage Handler With Shields Data")]
public class DamageHandlerWithShieldsData : ScriptableObject
{
    [Range(0f, 1f), Tooltip("How much damage will be absorbed by the shield")]
    public float shieldPercentageAbsorption = 0.5f;
    public float shieldCrushMuliplierDamage = 4f;
}
