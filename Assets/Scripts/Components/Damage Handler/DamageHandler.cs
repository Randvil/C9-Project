using UnityEngine;
using UnityEngine.Events;

public class DamageHandler : MonoBehaviour, IDamageHandler
{
    private IStats stats;

    private UnityEvent<float> takeDamageEvent = new();
    public UnityEvent<float> TakeDamageEvent { get => takeDamageEvent; }
    private UnityEvent dieEvent = new();
    public UnityEvent DieEvent { get => dieEvent; }


    private void Start()
    {
        stats = GetComponent<IStats>();
    }

    public void TakeDamage(Damage incomingDamage, IDamageReduced weapon)
    {
        float damageDone = incomingDamage.value;

        float currentHealth = stats.GetStat(eStatType.CurrentHealth);
        currentHealth -= incomingDamage.value;

        if (currentHealth <= 0f)
        {
            damageDone += currentHealth;

            currentHealth = 0f;
            dieEvent.Invoke();
        }

        stats.SetStat(eStatType.CurrentHealth, currentHealth);

        takeDamageEvent.Invoke(damageDone);
    }
}
