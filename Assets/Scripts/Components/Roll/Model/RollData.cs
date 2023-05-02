using UnityEngine;

[CreateAssetMenu(fileName = "NewRollData", menuName = "Data/Components/Roll/New Roll Data")]
public class RollData : ScriptableObject
{
    [Min(0f)]
    public float speed = 5f;
    [Min(0f)]
    public float duration = 0.6f;
    [Min(0f)]
    public float cooldown = 0.6f;
    [Range(0f, 1f)]
    public float colliderSizeMultiplier = 0.25f;
    [Range(0f, 1f), Tooltip("How much damage will be absorbed while rolling [parts of whole damage]")]
    public float damageAbsorption = 1f;
}
