using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMinion : BaseCreature, IPatrollingBehavior
{
    [Header("Spider Minion Prefab Components")]
    [SerializeField] protected Transform checkPlatformRightTransform;
    [SerializeField] protected Transform checkPlatformLeftTransform;
    [SerializeField] protected AudioSource movementAudioSource;

    [Header("Spider Minion Data")]
    [SerializeField] protected MovementData movementData;
    [SerializeField] protected EnergyManagerData energyManagerData;
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected OffensiveJumpData offensiveJumpData;
    [SerializeField] protected SpiderMinionCompoundAttackData spiderMinionCompoundAttackData;
    [SerializeField] protected PatrolmanStrategyData patrolmanStrategyData;

    [SerializeField] protected NoArmsWeaponViewData weaponViewData;
    [SerializeField] protected CommonAbilityViewData jumpViewData;

    public IMovement Movement { get; protected set; }
    public IModifierManager WeaponModifierManager { get; protected set; }
    public IEnergyManager EnergyManager { get; protected set; }
    public IWeapon Weapon { get; protected set; }
    public IAbility JumpAbility { get; protected set; }
    public ICompoundAttack CompoundAttack { get; protected set; }

    protected NoArmsWeaponView weaponView;
    protected CommonAbilityView jumpAbilityView;

    public Transform CheckPlatformRightTransform => checkPlatformRightTransform;
    public Transform CheckPlatformLeftTransform => checkPlatformLeftTransform;
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

        MovementView = new MovementView(Movement, Animator, movementAudioSource);
        weaponView = new NoArmsWeaponView(weaponViewData, Weapon, Animator, sharedAudioSource);
        jumpAbilityView = new CommonAbilityView(jumpViewData, JumpAbility, Animator, sharedAudioSource);

        currentBehavior = new PatrolmanStrategy(this, this);
        currentBehavior.Activate();

        DeathManager.DeathEvent.AddListener(OnDeath);
        DeathManager.DeathEvent.AddListener(GetComponent<EnemyVisualEffect>().ApplyDissolve);
        DamageHandler.TakeDamageEvent.AddListener(GetComponent<EnemyVisualEffect>().ApplyHurtEffect);
    }
}
