using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSelectiveEffectManagerData", menuName = "Data/Components/Managers/New Selective Effect Manager Data")]
public class SelectiveEffectManagerData : EffectManagerData
{
    [Tooltip("Effects of this type or higher will be applied to the character")]
    public eEffectPower susceptibilityType = eEffectPower.Weak;
}
