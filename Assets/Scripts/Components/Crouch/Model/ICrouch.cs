using UnityEngine.Events;

public interface ICrouch
{
    public void StartCrouch();
    public void BreakCrouch();
    public float CrouchSpeed { get; }
    public UnityEvent StartCrouchEvent { get; }
    public UnityEvent BreakCrouchEvent { get; }
}
