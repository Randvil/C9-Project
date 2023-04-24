using UnityEngine.Events;

public interface IWeapon
{
    public void StartAttack();
    public void BreakAttack();
    public bool IsAttacking { get; }
    public float AttackSpeed { get; }
    public float AttackRange { get; }
    public UnityEvent StartAttackEvent { get; }
    public UnityEvent BreakAttackEvent { get; }
    public UnityEvent ReleaseAttackEvent { get; }
}