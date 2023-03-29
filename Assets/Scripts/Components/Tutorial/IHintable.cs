public interface IHintable
{
    public UnityEngine.Events.UnityEvent ShowHint { get; }

    /// <summary>
    /// For UI-builder, set in prefab
    /// </summary>
    public string HintType { get; }
}