using UnityEngine;

[CreateAssetMenu(fileName = "New Roll Data", menuName = "Component Data/Model/New Roll Data", order = 180)]
public class RollData : ScriptableObject
{
    public float speed = 5f;
    public float duration = 0.6f;
    public float cooldown = 0.6f;
    public float colliderSizeMultiplier = 0.25f;
    public float damageAbsorption = 1f;
}
