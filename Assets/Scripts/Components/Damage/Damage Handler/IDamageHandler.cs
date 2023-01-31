using UnityEngine.Events;

public interface IDamageHandler
{
    public void TakeDamage(Damage incomingDamage);
    public void AddDamageModificator(IDamageModificator modificator);
    public void RemoveDamageModificator(IDamageModificator modificator);
    public void AddDamageEffect(IDamageEffect effect);
    public void RemoveDamageEffect(IDamageEffect effect);
    public UnityEvent<Damage, Damage> TakeDamageEvent { get; }
    public UnityEvent DieEvent { get; }
}
