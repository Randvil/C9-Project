using UnityEngine;

[CreateAssetMenu(fileName = "NewEffectManagerData", menuName = "Data/Components/Managers/New Effect Manager Data")]
public class EffectManagerData : ScriptableObject
{
    [Min(0f), Tooltip("How often effects will be checked for impact or ending")]
    public float checkEffectsPeriod = 0.25f;
}
