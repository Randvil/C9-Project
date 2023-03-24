using UnityEngine.Events;

public interface IDamageEffect : IEffect
{
    public void ApplyEffect(Damage incomingDamage);
    public UnityEvent DamageEffectEvent { get; }
}
