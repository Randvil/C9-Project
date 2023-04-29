using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractDamageAbility : AbstractAbility, IDamageAbility, IDamageDealer
{
    protected GameObject caster;

    protected DamageData damageData;

    protected IModifierManager modifierManager;
    protected ITurning turning;
    protected ITeam team;

    public UnityEvent<DamageInfo> DealDamageEvent { get; } = new();

    public float AttackRange { get; protected set; } = float.MaxValue;

    public AbstractDamageAbility(MonoBehaviour owner, GameObject caster, DamageAbilityData damageAbilityData, IEnergyManager energyManager, IModifierManager modifierManager, ITurning turning, ITeam team) : base(owner, damageAbilityData, energyManager)
    {
        this.caster = caster;

        damageData = damageAbilityData.damageData;

        this.modifierManager = modifierManager;
        this.turning = turning;
        this.team = team;
    }

}
