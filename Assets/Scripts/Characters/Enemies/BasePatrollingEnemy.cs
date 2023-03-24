using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePatrollingEnemy : MonoBehaviour, ITeam, IDamageable, IEffectable
{
    [SerializeField] private GameObject avatar;
    [SerializeField] private Transform checkPlatformAheadTransform;
    [SerializeField] private Slider healthBarSlider;

    [SerializeField] private TurningViewData turningViewData;
    [SerializeField] private MovementData movementData;
    [SerializeField] private HealthManagerData healthManagerData;
    [SerializeField] private EffectManagerData effectManagerData;
    [SerializeField] private SillyPatrolmanData sillyPatrolmanData;

    public BoxCollider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Gravity Gravity { get; private set; }
    public ITurning Turning { get; private set; }
    public ITurningView TurningView { get; private set; }
    public IMovement Movement { get; private set; }
    public abstract IWeapon Weapon { get; protected set; }
    public IHealthManager HealthManager { get; private set; }
    public IModifierManager WeaponModifierManager { get; private set; }
    public IModifierManager DefenceModifierManager { get; private set; }
    public IEffectManager EffectManager { get; private set; }
    public IDamageHandler DamageHandler { get; private set; }
    public IDeathManager DeathManager { get; private set; }
    public IEnemyBehavior EnemyBehavior { get; private set; }
    public HealthBarView HealthBarView { get; private set; }

    public eTeam Team { get; } = eTeam.Enemies;

    private bool stunned = false;

    protected virtual void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Gravity = GetComponent<Gravity>();

        HealthManager = new HealthManager(healthManagerData);
        WeaponModifierManager = new ModifierManager();
        DefenceModifierManager= new ModifierManager();
        EffectManager = new EffectManager(effectManagerData);
        DamageHandler = new DamageHandler(HealthManager, DefenceModifierManager, EffectManager);
        DeathManager = new DeathManager(HealthManager);
        Turning = new Turning();
        Movement = new Movement(movementData, Rigidbody, Turning, EffectManager);
        EnemyBehavior = new SillyPatrolman(gameObject, checkPlatformAheadTransform, sillyPatrolmanData, Collider, Turning, DamageHandler);

        TurningView = new TurningView(avatar, turningViewData, Turning);
        HealthBarView = new(healthBarSlider, HealthManager, DeathManager);

        EffectManager.EffectEvent.AddListener(OnStun);
        DeathManager.DeathEvent.AddListener(OnDie);

        EnemyBehavior.DirectionalMoveEvent.AddListener(OnDirectionalMove);
        EnemyBehavior.TurnEvent.AddListener(OnTurn);
        EnemyBehavior.StopEvent.AddListener(OnStop);
        EnemyBehavior.AttackEvent.AddListener(OnAttack);

        EnemyBehavior.Activate();
    }

    private void Update()
    {
        TurningView.Turn();
    }

    private void OnDirectionalMove(eDirection direction)
    {
        if (stunned)
        {
            return;
        }

        if (direction != Turning.Direction || Movement.IsMoving == false)
        {
            Turning.Turn(direction);
            Movement.StartMove();
        }
    }

    private void OnTurn(eDirection direction)
    {
        if (stunned)
        {
            return;
        }

        Turning.Turn(direction);
    }

    private void OnStop()
    {
        if (stunned)
        {
            return;
        }

        if (Movement.IsMoving == true)
        {
            Movement.StopMove();
        }
    }

    private void OnAttack()
    {
        if (stunned)
        {
            return;
        }

        Weapon.StartAttack();
    }

    private void OnStun(eEffectType effectType, eEffectStatus effectStatus)
    {
        if (effectType != eEffectType.Stun)
        {
            return;
        }

        if (effectStatus == eEffectStatus.Added)
        {
            stunned = true;
            Movement.StopMove();
            Weapon.BreakAttack();
        }
        else if (effectStatus == eEffectStatus.Cleared)
        {
            stunned = false;
            //Movement.StartMove();
        }
    }

    private void OnDie()
    {
        EnemyBehavior.Deactivate();

        Movement.StopMove();
        Weapon.BreakAttack();

        Destroy(gameObject, 1.5f);
    }

}