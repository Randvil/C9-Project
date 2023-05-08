using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMinion : BaseCreature, IPatrollingBehavior
{
    [Header("Spider Minion Prefab Components")]
    [SerializeField] protected Transform checkPlatformAheadTransform;
    [SerializeField] protected AudioSource movementAudioSource;
    [SerializeField] protected AudioSource sharedAudioSource;

    [Header("Spider Minion Data")]
    [SerializeField] protected MovementData movementData;
    [SerializeField] protected EnergyManagerData energyManagerData;
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected OffensiveJumpData offensiveJumpData;
    [SerializeField] protected SpiderMinionCompoundAttackData spiderMinionCompoundAttackData;
    [SerializeField] protected PatrolmanStrategyData patrolmanStrategyData;

    [SerializeField] protected NoArmsWeaponViewData weaponViewData;

    public IMovement Movement { get; protected set; }
    public IModifierManager WeaponModifierManager { get; protected set; }
    public IEnergyManager EnergyManager { get; protected set; }
    public IWeapon Weapon { get; protected set; }
    public IAbility JumpAbility { get; protected set; }
    public ICompoundAttack CompoundAttack { get; protected set; }

    protected NoArmsWeaponView weaponView;

    protected IAIBehavior currentBehavior;

    public Transform CheckPlatformAheadTransform => checkPlatformAheadTransform;
    public PatrolmanStrategyData PatrolmanStrategyData => patrolmanStrategyData;


    protected override void Awake()
    {
        base.Awake();

        Movement = new Movement(movementData, Rigidbody, Turning, EffectManager);
        WeaponModifierManager = new ModifierManager();
        EnergyManager = new EnergyManager(energyManagerData);
        Weapon = new SingleTargetMeleeWeapon(this, gameObject, weaponData, WeaponModifierManager, CharacterTeam, Turning);
        JumpAbility = new OffensiveJump(this, offensiveJumpData, EnergyManager, Rigidbody, Collider, Gravity, Turning, CharacterTeam, WeaponModifierManager);
        CompoundAttack = new SpiderMinionCompoundAttack(gameObject, spiderMinionCompoundAttackData, Weapon, JumpAbility);

        MovementView = new AnimationAndSoundMovementView(Movement, Animator, movementAudioSource);
        weaponView = new NoArmsWeaponView(weaponViewData, Weapon, Animator, sharedAudioSource);

        currentBehavior = new PatrolmanStrategy(this);
        currentBehavior.Activate();

        DeathManager.DeathEvent.AddListener(OnDeath);
    }

    protected void Update()
    {
        currentBehavior.LogicUpdate();
    }

    protected void FixedUpdate()
    {
        currentBehavior.PhysicsUpdate();
    }

    protected void OnDeath()
    {
        currentBehavior.Deactivate();
        Destroy(gameObject, 0.5f);
    }
}
