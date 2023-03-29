using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDamageAbility : AbstractAbility, IAbility
{
    protected GameObject caster;

    protected DamageAbilityData damageAbilityData;

    protected IModifierManager modifierManager;
    protected ITurning turning;
    protected ITeam team;

    public AbstractDamageAbility(GameObject caster, DamageAbilityData damageAbilityData, IAbilityManager abilityManager, IEnergyManager energyManager, IModifierManager modifierManager, ITurning turning, ITeam team) : base(damageAbilityData, abilityManager, energyManager)
    {
        this.caster = caster;

        this.damageAbilityData = damageAbilityData;

        this.modifierManager = modifierManager;
        this.turning = turning;
        this.team = team;
    }
}
