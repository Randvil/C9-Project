using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaTessen : AbstractAbility, IDamageDealer
{
    protected GameObject ascensionAreaPrefab;
    protected float areaSpawnDistance;

    protected ITurning turning;
    protected IModifierManager abilityModifierManager;
    protected ITeam team;

    public UnityEvent<DamageInfo> DealDamageEventCallback { get; } = new();


    public AreaTessen(MonoBehaviour owner, AreaTessenData areaTessenData, IEnergyManager energyManager, ITurning turning, IModifierManager abilityModifierManager, ITeam team) : base(owner, areaTessenData, energyManager)
    {
        ascensionAreaPrefab = areaTessenData.ascensionAreaPrefab;
        areaSpawnDistance = areaTessenData.areaSpawnDistance;

        this.turning = turning;
        this.abilityModifierManager = abilityModifierManager;
        this.team = team;
    }


    protected override IEnumerator ReleaseStrikeCoroutine()
    {
        Vector2 characterPosition = owner.transform.position;
        float shift = turning.Direction == eDirection.Right ? areaSpawnDistance : -areaSpawnDistance;
        Vector2 ascensionAreaPosition = characterPosition + new Vector2(shift, 0f);

        AscensionArea ascensionArea = Object.Instantiate(ascensionAreaPrefab, ascensionAreaPosition, Quaternion.identity).GetComponent<AscensionArea>();
        ascensionArea.Initialize(owner.gameObject, abilityModifierManager, team, this);

        energyManager.ChangeCurrentEnergy(-cost);
        finishCooldownTime = Time.time + cooldown;
        ReleaseCastEvent.Invoke();

        yield return null;
    }
}
