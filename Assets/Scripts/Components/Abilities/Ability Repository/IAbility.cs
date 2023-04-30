using UnityEngine.Events;

public interface IAbility
{
    public void StartCast();
    public void BreakCast();
    public eAbilityType Type { get; }
    public bool CanBeUsed { get; }
    public bool IsPerforming { get; }
    public float Cooldown { get; }
    public UnityEvent StartCastEvent { get; }
    public UnityEvent BreakCastEvent { get; }
    public UnityEvent ReleaseCastEvent { get; }
}
