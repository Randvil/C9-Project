using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodmotherStrategy : BaseStrategy
{
    private IBroodmotherBehavior broodmother;

    private BroodmotherStrategyData broodmotherStrategyData;
    private float phaseTwoThresholdRelativeHealth;
    private float phaseThreeThresholdRelativeHealth;
    private Vector2 recoveryPosition;

    private IMovement movement;
    private IHealthManager healthManager;
    private IHealthManager shieldManager;
    private IClimb climb;

    public int CurrentPhase { get; set; } = 1;

    public IState RetreatingState { get; private set; }

    public BroodmotherStrategy(MonoBehaviour owner, IBroodmotherBehavior broodmother, GameObject enemy) : base(owner)
    {
        this.broodmother = broodmother;
        Enemy = enemy;

        broodmotherStrategyData = broodmother.BroodmotherStrategyData;
        phaseTwoThresholdRelativeHealth = broodmotherStrategyData.phaseTwoThresholdRelativeHealth;
        phaseThreeThresholdRelativeHealth = broodmotherStrategyData.phaseThreeThresholdRelativeHealth;
        recoveryPosition = broodmotherStrategyData.recoveryPosition;

        collider = broodmother.Collider;
        characterTeam = broodmother.CharacterTeam;
        damageHandler = broodmother.DamageHandler;
        effectManager = broodmother.EffectManager;
        deathManager = broodmother.DeathManager;
        turning = broodmother.Turning;
        movement = broodmother.Movement;
        climb = broodmother.Climb;
        CompoundAttack = broodmother.CompoundAttack;
        healthManager = broodmother.HealthManager;
        shieldManager = broodmother.ShieldManager;

        StateMachine = new StateMachine();
        IdleState = new BroodmotherIdleState(this);
        AttackingState = new BroodmotherAttackingState(this);
        RetreatingState = new BroodmotherRetreatingState(this);
        StunnedState = new BroodmotherStunnedState();
        DyingState = new BroodmotherDyingState();
    }

    public bool ChangePhase()
    {
        switch (CurrentPhase)
        {
            case 1:
                if (healthManager.Health.currentHealth < healthManager.Health.maxHealth * phaseTwoThresholdRelativeHealth)
                {
                    CurrentPhase = 2;

                    if (CompoundAttack is BroodmotherCompoundAttack broodmotherAttack)
                    {
                        broodmotherAttack.ChangePhase(CurrentPhase);
                    }

                    return true;
                }
                break;

            case 2:
                if (healthManager.Health.currentHealth < healthManager.Health.maxHealth * phaseThreeThresholdRelativeHealth)
                {
                    CurrentPhase = 3;

                    if (CompoundAttack is BroodmotherCompoundAttack broodmotherAttack)
                    {
                        broodmotherAttack.ChangePhase(CurrentPhase);
                    }

                    return true;
                }
                break;
        }

        return false;
    }

    public void Move()
    {
        movement.StartMove();
    }

    public void Stop()
    {
        movement.BreakMove();
    }

    public bool Retreat()
    {
        if (collider.transform.position.y - recoveryPosition.y > 0f)
        {
            climb.StopClimb();
            broodmother.RegenerationAbility.StartCast();

            if (shieldManager.Health.currentHealth == shieldManager.Health.maxHealth)
            {
                return false;
            }

            return true;
        }

        if (climb.IsClimbing)
        {
            climb.ClimbUp();
            return true;
        }

        if (Mathf.Abs(collider.transform.position.x - recoveryPosition.x) < 0.1f)
        {
            Stop();
            climb.StartClimb();
            return true;
        }

        TurnToPoint(recoveryPosition.x);
        Move();

        return true;
    }

    public void BreakRetreat()
    {
        broodmother.RegenerationAbility.BreakCast();
        climb.BreakClimb();
    }

    protected override void OnTakeDamage(DamageInfo damageInfo)
    {
        //do nothing
    }
}
