using UnityEngine;

[CreateAssetMenu(fileName = "NewJumpData", menuName = "Data/Components/Jump/New Jump Data")]
public class JumpData : ScriptableObject
{
    [Min(0f)]
    public float maxSpeed = 10f;
    public AnimationCurve speedCurve;
    [Min(0f)]
    public float jumpTime = 0.3f;
    [Min(1)]
    public int maxJumpCount = 2;

}
