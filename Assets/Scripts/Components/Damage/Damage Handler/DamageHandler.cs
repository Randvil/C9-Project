using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageHandler : MonoBehaviour, IDamageHandler
{
    private List<IDamageModificator> modificators = new();
    private bool haveToSortModificators = false;

    private List<IDamageEffect> effects = new();

    private UnityEvent<Damage, Damage> takeDamageEvent = new();
    public UnityEvent<Damage, Damage> TakeDamageEvent { get => takeDamageEvent; }

    private UnityEvent dieEvent = new();
    public UnityEvent DieEvent { get => dieEvent; }


    private IStats stats;


    private void Start()
    {
        stats = GetComponent<IStats>();
    }

    public void TakeDamage(Damage incomingDamage)
    {
        // calculate effective damage
        Damage effectiveIncomingDamage = incomingDamage;

        if (incomingDamage.modificators.Count > 0)
        {
            incomingDamage.modificators.Sort();

            foreach (IDamageModificator modificator in incomingDamage.modificators)
            {
                effectiveIncomingDamage = modificator.ApplyModificator(effectiveIncomingDamage);
            }
        }

        // calculate outgoing damage
        Damage outgoingDamage = effectiveIncomingDamage;

        // apply effects
        if (effects.Count > 0)
        {
            foreach (IDamageEffect effect in effects)
            {
                effect.ApplyEffect(outgoingDamage);
            }
        }

        // apply modificators
        if (modificators.Count > 0)
        {
            if (haveToSortModificators)
            {
                modificators.Sort();
                haveToSortModificators = false;
            }

            foreach (IDamageModificator modificator in modificators)
            {
                outgoingDamage = modificator.ApplyModificator(outgoingDamage);
            }
        }

        // change health
        float currentHealth = stats.GetStat(eStatType.CurrentHealth);
        currentHealth -= outgoingDamage.baseDamage;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            dieEvent.Invoke();
        }

        stats.SetStat(eStatType.CurrentHealth, currentHealth);

        takeDamageEvent.Invoke(effectiveIncomingDamage, outgoingDamage);
    }

    public void AddDamageModificator(IDamageModificator modificator)
    {
        modificators.Add(modificator);
        haveToSortModificators = true;
    }

    public void RemoveDamageModificator(IDamageModificator modificator)
    {
        modificators.Remove(modificator);
    }

    public void AddDamageEffect(IDamageEffect effect)
    {
        effects.Add(effect);
    }

    public void RemoveDamageEffect(IDamageEffect effect)
    {
        effects.Remove(effect);
    }
}
