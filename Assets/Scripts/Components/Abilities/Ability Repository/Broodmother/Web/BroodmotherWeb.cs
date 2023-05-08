using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BroodmotherWeb : AbstractDamageAbility
{
    protected Transform[] projectileSpawnPoints;
    protected GameObject projectilePrefab;
    protected int webSeries;
    protected float seriesDelay;

    protected int currentWeb;

    public BroodmotherWeb(MonoBehaviour owner, GameObject caster, Transform[] projectileSpawnPoints, BroodmotherWebData broodmotherWebData, IEnergyManager energyManager, IModifierManager modifierManager, ITurning turning, ITeam team) : base(owner, caster, broodmotherWebData, energyManager, modifierManager, turning, team)
    {
        this.projectileSpawnPoints = projectileSpawnPoints;
        projectilePrefab = broodmotherWebData.projectilePrefab;
        webSeries = broodmotherWebData.webSeries;
        seriesDelay = broodmotherWebData.seriesDelay;
    }

    protected override IEnumerator ReleaseStrikeCoroutine()
    {
        currentWeb = 0;
        while(currentWeb < webSeries)
        {
            Vector2 randomPosition = projectileSpawnPoints[Random.Range(0, projectileSpawnPoints.Length)].position;
            IProjectile projectile = Object.Instantiate(projectilePrefab, randomPosition, Quaternion.Euler(new Vector3(0f, (float)turning.Direction, 0f))).GetComponent<IProjectile>();
            projectile.Release(caster, damageData, turning.Direction, team, modifierManager, this);

            currentWeb++;
            energyManager.ChangeCurrentEnergy(-cost);
            finishCooldownTime = Time.time + cooldown;
            ReleaseCastEvent.Invoke();

            if (currentWeb != webSeries)
            {
                yield return new WaitForSeconds(seriesDelay);
            }
        }        
    }
}
