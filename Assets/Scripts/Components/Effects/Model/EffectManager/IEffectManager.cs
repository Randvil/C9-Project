using UnityEngine.Events;

public interface IEffectManager
{
    public void AddEffect(IEffect effect);
    public void RemoveEffect(IEffect effect);
    public void ClearEffects(eEffectType effectType);
    public void ApplyDamageEffects(Damage damage);
    public float GetMaxStunDuration();
    public float GetCumulativeSlowEffect();
    public float GetCumulativeDamagePerSecond();
    public UnityEvent<eEffectType, eEffectStatus> EffectEvent { get; }
}
