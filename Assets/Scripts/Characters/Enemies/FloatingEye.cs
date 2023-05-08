using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEye : BaseCreature, IPatrollingBehavior
{
    [Header("Floating Eye Prefab Components")]
    [SerializeField] protected Transform checkPlatformAheadTransform;
    [SerializeField] protected AudioSource movementAudioSource;
    [SerializeField] protected AudioSource sharedAudioSource;

    [Header("Floating Eye Data")]
    [SerializeField] protected MovementData movementData;
    [SerializeField] protected EnergyManagerData energyManagerData;
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected InvisibilityData invisibilityData;
    [SerializeField] protected PatrolmanStrategyData patrolmanStrategyData;

    [SerializeField] protected NoArmsWeaponViewData weaponViewData;

    public IMovement Movement { get; protected set; }
    public IModifierManager WeaponModifierManager { get; protected set; }
    public IEnergyManager EnergyManager { get; protected set; }
    public IWeapon Weapon { get; protected set; }
    public ICompoundAttack CompoundAttack { get; protected set; }
    public IAbility Invisibility { get; protected set; }

    protected NoArmsWeaponView weaponView;
    protected InvisibilityView invisibilityView;

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
        CompoundAttack = new JustWeaponCompoundAttack(gameObject, Weapon);
        Invisibility = new Invisibility(this, invisibilityData, EnergyManager, DamageHandler, Weapon);

        MovementView = new AnimationAndSoundMovementView(Movement, Animator, movementAudioSource);
        weaponView = new NoArmsWeaponView(weaponViewData, Weapon, Animator, sharedAudioSource);
        invisibilityView = new InvisibilityView(this, Invisibility as Invisibility, meshesToCloneMaterials);

        currentBehavior = new PatrolmanStrategy(this);
        currentBehavior.Activate();

        DeathManager.DeathEvent.AddListener(OnDeath);

        Invisibility.StartCast();
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
