using UnityEngine;

[CreateAssetMenu(fileName = "New Jump Data", menuName = "Component Data/Model/New Jump Data", order = 150)]
public class JumpData : ScriptableObject
{
    public float maxSpeed = 10f;
    public AnimationCurve speedCurve;
    public float jumpTime = 0.3f;
    public int maxJumpCount = 2;

}
