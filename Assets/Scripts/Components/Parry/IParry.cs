using UnityEngine;
using UnityEngine.Events;

public interface IParry
{
    //if the player can parry more damage
    public Damage ParryDamage { get; }
    public float ParryRadius { get; }
    public float ParryDelay { get; }
    public void StartParry(Vector3 direction);
    public void StopParry(Vector3 direction);
    public bool IsParrying { get; }
    public UnityEvent StartParryEvent { get; }
    public UnityEvent StopParryEvent { get; }
    public UnityEvent<IDamageReduced> WeaponWasParriedEvent { get; }
}
