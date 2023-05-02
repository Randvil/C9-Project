using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LastChance : ITalent
{
    private MonoBehaviour owner;

    private int initialCharges;
    private float invincibilityDuration;

    private IForbiddableDeath deathManager;
    private IHealthManager healthManager;
    private IModifierManager defenceModifierManager;

    private int currentCharges;
    private IDamageModifier damageModifier;
    private Coroutine lastChanceCoroutine;

    public eTalentType TalentType { get; } = eTalentType.LastChance;
    public bool Active => lastChanceCoroutine != null;
    public UnityEvent ActivationEvent { get; } = new();
    public UnityEvent<eTalentType> LearnEvent { get; } = new();
    public UnityEvent<eTalentType> ForgetEvent { get; } = new();

    public LastChance(MonoBehaviour owner, LastChanceData lastChanceData, IForbiddableDeath deathManager, IHealthManager healthManager, IModifierManager defenceModifierManager)
    {
        this.owner = owner;

        initialCharges = lastChanceData.initialCharges;
        invincibilityDuration = lastChanceData.invincibilityDuration;
        this.deathManager = deathManager;
        this.healthManager = healthManager;
        this.defenceModifierManager = defenceModifierManager;

        damageModifier = new RelativeDamageModifier(1f);
    }

    public void Learn()
    {
        currentCharges = initialCharges;
        deathManager.ForbidDying(this);
        deathManager.PreventedDeathEvent.AddListener(OnPreventedDeath);
    }

    public void Forget()
    {
        deathManager.AllowDying(this);
        deathManager.PreventedDeathEvent.RemoveListener(OnPreventedDeath);
    }

    private void OnPreventedDeath()
    {
        if (currentCharges == 0)
        {
            return;
        }

        if (Active)
        {
            return;
        }

        lastChanceCoroutine = owner.StartCoroutine(LastChanceCoroutine());
        ActivationEvent.Invoke();
    }

    private IEnumerator LastChanceCoroutine()
    {
        currentCharges--;

        if (healthManager.Health.currentHealth <= 1f)
        {
            healthManager.SetCurrentHealth(1f);
        }

        defenceModifierManager.AddModifier(damageModifier);

        yield return new WaitForSeconds(invincibilityDuration);

        defenceModifierManager.RemoveModifier(damageModifier);

        if (currentCharges == 0)
        {
            deathManager.AllowDying(this);
        }

        lastChanceCoroutine = null;
    }
}
