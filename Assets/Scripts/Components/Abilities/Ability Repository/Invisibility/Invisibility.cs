using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Invisibility : AbstractAbility
{
    protected float duration;
    protected float checkFinishEffectPeriod;

    protected IDamageHandler damageHandler;
    protected IWeapon weapon;

    protected float finishInvisibilityTime;
    protected Coroutine invisibilityCoroutine;

    public bool IsInvisible { get; protected set; }
    public float FadeTime { get; protected set; }
    public float AppearanceTime { get; protected set; }

    public UnityEvent StartFadeEvent { get; protected set; } = new();
    public UnityEvent BreakInvisibilityEvent { get; protected set; } = new();

    public Invisibility(MonoBehaviour owner, InvisibilityData invisibilityData, IEnergyManager energyManager, IDamageHandler damageHandler, IWeapon weapon) : base(owner, invisibilityData, energyManager)
    {
        duration = invisibilityData.duration;
        checkFinishEffectPeriod = invisibilityData.checkFinishEffectPeriod;
        FadeTime = invisibilityData.fadeTime;
        AppearanceTime = invisibilityData.appearanceTime;

        this.damageHandler = damageHandler;
        this.weapon = weapon;
    }

    protected override IEnumerator ReleaseStrikeCoroutine()
    {
        StartFadeEvent.Invoke();

        damageHandler.TakeDamageEvent.AddListener(OnTakeDamage);
        weapon.ReleaseAttackEvent.AddListener(BreakInvisibility);

        yield return new WaitForSeconds(FadeTime);
        
        IsInvisible = true;
        finishInvisibilityTime = Time.time + duration;
        finishCooldownTime = Time.time + cooldown;
        invisibilityCoroutine = owner.StartCoroutine(InvisibilityCoroutine());
        energyManager.ChangeCurrentEnergy(-cost);
        ReleaseCastEvent.Invoke();
    }

    protected void OnTakeDamage(DamageInfo damageInfo)
    {
        if (damageInfo.effectiveDamageValue > 0f)
        {
            BreakInvisibility();
        }
    }

    protected void BreakInvisibility()
    {
        BreakCast();
        IsInvisible = false;
        damageHandler.TakeDamageEvent.RemoveListener(OnTakeDamage);
        weapon.ReleaseAttackEvent.RemoveListener(BreakInvisibility);
        if (invisibilityCoroutine != null)
        {
            owner.StopCoroutine(invisibilityCoroutine);
            invisibilityCoroutine = null;
        }        
        BreakInvisibilityEvent.Invoke();
    }

    protected IEnumerator InvisibilityCoroutine()
    {
        while (Time.time < finishInvisibilityTime)
        {
            yield return new WaitForSeconds(checkFinishEffectPeriod);
        }

        BreakInvisibility();
    }
}
