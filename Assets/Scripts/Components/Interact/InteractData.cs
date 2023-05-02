using UnityEngine;

[CreateAssetMenu(fileName = "NewInteractData", menuName = "Data/Components/Interaction/New Interact Data")]
public class InteractData : ScriptableObject
{
    [Min(0.01f), Tooltip("How often will the component scan space to find an interactive object")]
    public float searchPeriod = 0.1f;
    public LayerMask interactiveObjectLayer;
}
