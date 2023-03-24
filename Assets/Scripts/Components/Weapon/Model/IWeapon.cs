using UnityEngine.Events;

public interface IWeapon
{
    public void StartAttack();
    public void BreakAttack();
    public bool IsAttacking { get; }
    public UnityEvent ReleaseAttackEvent { get; }
}