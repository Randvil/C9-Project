using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePatrollingEnemy : MonoBehaviour, ITeam, IDamageable, IEffectable
{
    [SerializeField] protected GameObject avatar;
    [SerializeField] protected Transform checkPlatformAheadTransform;
    [SerializeField] protected Slider healthBarSlider;
    [SerializeField] protected AudioSource movementAudioSource;
    [SerializeField] protected AudioSource attackAudioSource;

    [SerializeField] protected TurningViewData turningViewData;
    [SerializeField] protected MovementData movementData;
    [SerializeField] protected HealthManagerData healthManagerData;
    [SerializeField] protected EffectManagerData effectManagerData;
    [SerializeField] protected SillyPatrolmanData sillyPatrolmanData;

    public BoxCollider2D Collider { get; protected set; }
    public Rigidbody2D Rigidbody { get; protected set; }
    public Gravity Gravity { get; protected set; }
    public Animator Animator { get; protected set; }
    public ITurning Turning { get; protected set; }
    public IMovement Movement { get; protected set; }
    public IWeapon Weapon { get; protected set; }
    public IHealthManager HealthManager { get; protected set; }
    public IModifierManager WeaponModifierManager { get; protected set; }
    public IModifierManager DefenceModifierManager { get; protected set; }
    public IEffectManager EffectManager { get; protected set; }
    public IDamageHandler DamageHandler { get; protected set; }
    public IDeathManager DeathManager { get; protected set; }
    public IEnemyBehavior EnemyBehavior { get; protected set; }

    public ITurningView TurningView { get; protected set; }
    public IMovementView MovementView { get; protected set; }
    public IWeaponView WeaponView { get; protected set; }
    public HealthBarView HealthBarView { get; protected set; }

    public eTeam Team { get; } = eTeam.Enemies;

    private bool stunned = false;

    protected virtual void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Gravity = GetComponent<Gravity>();
        Animator = GetComponent<Animator>();


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
        MovementView = new PatrollingCreatureMovementView(Movement, Animator, movementAudioSource);
        HealthBarView = new(healthBarSlider, HealthManager, DeathManager);

        CreateWeaponWithView();

        EffectManager.EffectEvent.AddListener(OnStun);
        DeathManager.DeathEvent.AddListener(OnDie);

        EnemyBehavior.DirectionalMoveEvent.AddListener(OnDirectionalMove);
        EnemyBehavior.TurnEvent.AddListener(OnTurn);
        EnemyBehavior.StopEvent.AddListener(OnStop);
        EnemyBehavior.AttackEvent.AddListener(OnAttack);

        EnemyBehavior.Activate();
    }

    protected abstract void CreateWeaponWithView();

    protected virtual void Update()
    {
        TurningView.Turn();
        MovementView.SetMovementParams();
    }

    protected virtual void OnDirectionalMove(eDirection direction)
    {
        if (stunned)
        {
            return;
        }

        if (direction != Turning.Direction || Movement.IsMoving == false)
        {
            Turning.Turn(direction);
            Movement.StartMove();
            WeaponView.BreakAttack();
        }
    }

    protected virtual void OnTurn(eDirection direction)
    {
        if (stunned)
        {
            return;
        }

        Turning.Turn(direction);
    }

    protected virtual void OnStop()
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

    protected virtual void OnAttack()
    {
        if (stunned)
        {
            return;
        }

        Weapon.StartAttack();
        WeaponView.StartAttack();
    }

    protected virtual void OnStun(eEffectType effectType, eEffectStatus effectStatus)
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
            WeaponView.BreakAttack();
        }
        else if (effectStatus == eEffectStatus.Cleared)
        {
            stunned = false;
            //Movement.StartMove();
        }
    }

    protected virtual void OnDie()
    {
        EnemyBehavior.Deactivate();

        Movement.StopMove();
        Weapon.BreakAttack();
        WeaponView.BreakAttack();

        Destroy(gameObject, 1.5f);
    }

}