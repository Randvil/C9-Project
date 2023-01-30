using UnityEngine.Events;

public interface IWeapon
{
    public Damage Damage { get; }
    public float AttackRadius { get; }
    public void StartAttack(eDirection direction);
    public void StopAttack();
    public bool IsAttacking { get; }
    public UnityEvent StartAttackEvent { get; }
    public UnityEvent ReleaseAttackEvent { get; }
    public UnityEvent StopAttackEvent { get; }
    public UnityEvent<int> EntityAttackEvent { get; }
}
