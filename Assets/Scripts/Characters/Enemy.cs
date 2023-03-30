using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITeam
{
    private eTeam team = eTeam.Enemies;
    public eTeam Team { get => team; }

    private IMovement movement;
    private ITurning turning;
    private IWeapon weapon;
    private IStats stats;
    private IDamageHandler damageHandler;
    private IEnemyBehavior enemyBehavior;

    private void Start()
    {
        movement = GetComponent<IMovement>();
        turning = GetComponent<ITurning>();
        weapon = GetComponent<IWeapon>();
        stats = GetComponent<IStats>();
        damageHandler = GetComponent<IDamageHandler>();
        enemyBehavior = GetComponent<IEnemyBehavior>();

        damageHandler.DieEvent.AddListener(OnDie);

        enemyBehavior.DirectionalMoveEvent.AddListener(OnDirectionalMove);
        enemyBehavior.TurnEvent.AddListener(OnTurn);
        enemyBehavior.StopEvent.AddListener(OnStop);
        enemyBehavior.AttackEvent.AddListener(OnAttack);

        enemyBehavior.Activate();
    }

    private void OnDirectionalMove(eDirection direction)
    {
        turning.Turn(direction);
        movement.StartMove(direction);
    }

    private void OnTurn(eDirection direction)
    {
        turning.Turn(direction);
    }

    private void OnStop()
    {
        movement.StopMove();
    }

    private void OnAttack()
    {
        weapon.StartAttack();
    }

    private void OnDie()
    {
        enemyBehavior.Deactivate();

        movement.StopMove();
        weapon.StopAttack();

        Destroy(gameObject, 1.5f);
    }
}