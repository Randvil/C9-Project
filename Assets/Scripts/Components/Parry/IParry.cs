using UnityEngine;
using UnityEngine.Events;

public interface IParry
{
    public eParryType ParryType { get; set; }
    public float DamageAbsorption { get; set; }
    public float Duration { get; set; }
    public float Cooldown { get; set; }
    public bool IsParrying { get; }
    public bool IsOnCooldown { get; }
    public void StartParry(eDirection direction);
    public void StopParry();
    public UnityEvent StartParryEvent { get; }
    public UnityEvent StopParryEvent { get; }
}
