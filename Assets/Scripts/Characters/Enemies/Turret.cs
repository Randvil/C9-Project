using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, ITeam
{
    [SerializeField] private GameObject avatar;
    [SerializeField] private Transform projectileSpawnPoint;

    [SerializeField] private TurningViewData turningViewData;
    [SerializeField] private RangedWeaponData rangedWeaponData;

    [SerializeField] private float searchEnemyDistance = 10f;

    private GameObject enemy;

    public ITurning Turning { get; private set; }
    public ITurningView TurningView { get; private set; }
    public IModifierManager ModifierManager { get; private set; }
    public IWeapon Weapon { get; private set; }

    public eTeam Team => eTeam.Enemies;

    private void Awake()
    {
        Turning = new Turning();
        TurningView = new TurningView(avatar, turningViewData, Turning);

        ModifierManager = new ModifierManager();
        Weapon = new OrdinaryBow(gameObject, projectileSpawnPoint, rangedWeaponData, ModifierManager, this, Turning);
    }

    private void FixedUpdate()
    {
        if (EnemyIsNear())
        {
            TurnToEnemy();
            Weapon.StartAttack();
            return;
        }

        enemy = null;
        SearchEnemy(Vector2.left, eDirection.Left);
        SearchEnemy(Vector2.right, eDirection.Right);

        bool EnemyIsNear()
        {
            return enemy != null && Vector2.Distance(enemy.transform.position, transform.position) < searchEnemyDistance;
        }
    }

    private void SearchEnemy(Vector2 direction, eDirection turnDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, searchEnemyDistance);
        if (hit.collider == null)
        {
            return;
        }
        
        if (hit.collider.TryGetComponent(out ITeam enemyTeam) == false || enemyTeam.Team == Team)
        {
            return;
        }

        enemy = hit.collider.gameObject;
        Turning.Turn(turnDirection);
    }

    private void TurnToEnemy()
    {
        float relativeEnemyPosition = enemy.transform.position.x - transform.position.x;
        if (relativeEnemyPosition > 0f)
        {
            Turning.Turn(eDirection.Right);
        }
        else
        {
            Turning.Turn(eDirection.Left);
        }

        TurningView.Turn();
    }
}
