using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageHandlerPlayer : MonoBehaviour, IDamageHandler
{
    private IStats stats;
    private IParry parry;
    private List<IDamageReduced> parriedWeapons = new();

    private UnityEvent<float> takeDamageEvent = new();
    public UnityEvent<float> TakeDamageEvent { get => takeDamageEvent; }

    private UnityEvent dieEvent = new();
    public UnityEvent DieEvent { get => dieEvent; }


    private void Start()
    {
        stats = GetComponent<IStats>();
        parry = GetComponent<IParry>();
        parry.WeaponWasParriedEvent.AddListener(ReduceDamage);
    }

    public void TakeDamage(Damage incomingDamage, IDamageReduced weapon)
    {
        float damageDone = incomingDamage.value;

        if (parriedWeapons.Contains(weapon))
        {
            damageDone -= weapon.DamageMinus.value;
            Debug.Log("damage of " + weapon + " was parried");
            parriedWeapons.Remove(weapon);
        }

        float currentHealth = stats.GetStat(eStatType.CurrentHealth);
        currentHealth -= damageDone;

        if (currentHealth <= 0f)
        {
            damageDone += currentHealth;

            currentHealth = 0f;
            dieEvent.Invoke();
        }

        stats.SetStat(eStatType.CurrentHealth, currentHealth);
        takeDamageEvent.Invoke(damageDone);
    }

    public void ReduceDamage(IDamageReduced weapon)
    {
        parriedWeapons.Add(weapon);
    }
}
