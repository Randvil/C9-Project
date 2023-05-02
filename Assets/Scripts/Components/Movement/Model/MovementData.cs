using UnityEngine;

[CreateAssetMenu(fileName = "NewMovementData", menuName = "Data/Components/Movement/New Movement Data")]
public class MovementData : ScriptableObject
{
    [Min(0f)]
    public float speed = 3f;
}
