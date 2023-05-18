using UnityEngine;

public abstract class Hintable : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent<string> ShowHint { get; } = new();
    public UnityEngine.Events.UnityEvent<string> HideHint { get; } = new();

    /// <summary>
    /// For UI-builder, set in prefab
    /// </summary>
    [field: SerializeField] public string LabelName { get; set; }
}