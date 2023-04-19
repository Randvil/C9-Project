using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daikyu : AbstractDamageAbility, ISustainableAbility
{
    protected DaikyuData daikyuData;

    private bool stopSustaining;
    private IDamageModifier damageModifier;

    public Daikyu(GameObject caster, DaikyuData daikyuData, IAbilityManager abilityManager, IEnergyManager energyManager, IModifierManager modifierManager, ITurning turning, ITeam team) : base(caster, daikyuData, abilityManager, energyManager, modifierManager, turning, team)
    {
        Type = eAbilityType.Daikyu;

        this.daikyuData = daikyuData;
    }

    public override void StartCast()
    {
        if (damageModifier != null)
        {
            modifierManager.RemoveModifier(damageModifier);
        }

        base.StartCast();
    }

    public override void BreakCast()
    {
        stopSustaining = false;

        base.BreakCast();
    }

    protected override IEnumerator ReleaseStrikeCoroutine()
    {
        yield return new WaitForSeconds(daikyuData.preCastDelay);

        float startChargeTime = Time.time;

        yield return new WaitUntil(() => (startChargeTime + daikyuData.fullChargeTime <= Time.time) || stopSustaining);

        float additionalDamage = (daikyuData.fullChargeDamageMultiplier - 1f) * Mathf.Clamp01((Time.time - startChargeTime) / daikyuData.fullChargeTime);
        damageModifier = new RelativeDamageModifier(1f + additionalDamage);

        modifierManager.AddModifier(damageModifier);

        IProjectile projectile = Object.Instantiate(daikyuData.projectileData.prefab, caster.transform.position, Quaternion.Euler(new Vector3(0f, (float)turning.Direction, 0f))).GetComponent<IProjectile>();
        projectile.Initialize(caster, daikyuData.damageData, daikyuData.projectileData, turning.Direction, team, modifierManager, this);

        energyManager.ChangeCurrentEnergy(-daikyuData.cost);

        finishCooldownTime = Time.time + daikyuData.cooldown;
        ReleaseCastEvent.Invoke();

        yield return new WaitForSeconds(daikyuData.postCastDelay);

        BreakCast();
    }

    public void StopSustaining()
    {
        if (IsPerforming)
        {
            stopSustaining = true;
        }            
    }
}
