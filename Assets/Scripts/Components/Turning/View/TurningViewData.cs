using UnityEngine;

[CreateAssetMenu(fileName = "NewTurningViewData", menuName = "Data/Components/Turning/View/New Turning View Data")]
public class TurningViewData : ScriptableObject
{
    [Min(0f), Tooltip("Speed of turning [degrees]")]
    public float turnSpeed = 1000f;
}
