using UnityEngine.Events;

public interface IRoll
{
    public void StartRoll(eDirection direction);
    public void StopRoll();
    public bool IsRolling { get; }
    public UnityEvent StartRollEvent { get; }
    public UnityEvent StopRollEvent { get; }
}
