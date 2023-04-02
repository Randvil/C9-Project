using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, ITeam, IDamageable, IEffectable, IAbilityCaster, IMortal, IClimbable, IDataSavable
{
    [SerializeField] private GameObject avatar;
    [SerializeField] private GameObject weaponObject;
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private Transform weaponGrip;
    [SerializeField] public PlayerInput unityPlayerInput;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource parrySound;

    [SerializeField] private HealthManagerData healthManagerData;
    [SerializeField] private EnergyManagerData energyManagerData;
    [SerializeField] private EffectManagerData effectManagerData;
    [SerializeField] private InteractData interactData;
    [SerializeField] private TurningViewData turningViewData;
    [SerializeField] private MovementData movementData;
    [SerializeField] private JumpData jumpData;
    [SerializeField] private CrouchData crouchData;
    [SerializeField] private ClimbData climbData;
    [SerializeField] private RollData rollData;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private EnergyRegeneratorData energyRegeneratorData;
    [SerializeField] private ParryData parryData;
    [SerializeField] private KanaboData kanaboData;
    [SerializeField] private DaikyuData daikyuData;
    [SerializeField] private TessenData tessenData;

    public eTeam Team { get; private set; } = eTeam.Player;
    public Transform CameraFollowPoint => avatar.transform;

    public BoxCollider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public IPlayerInput PlayerInput { get; set; }
    public IGravity Gravity { get; private set; }
    public IGravityView GravityView { get; private set; }
    public IMovement Movement { get; private set; }
    public ICrouch Crouch { get; private set; }
    public ITurning Turning { get; private set; }
    public IJump Jump { get; private set; }
    public IRoll Roll { get; private set; }
    public IModifierManager WeaponModifierManager { get; private set; }
    public IModifierManager AbilityModifierManager { get; private set; }
    public IModifierManager DefenceModifierManager { get; private set; }
    public IInteract Interact { get; private set; }
    public IWeapon Weapon { get; private set; }
    public IHealthManager HealthManager { get; private set; }
    public IEnergyManager EnergyManager { get; private set; }
    public IEnergyRegenerator WeaponEnergyRegenerator { get; private set; }
    public IAbilityManager AbilityManager { get; private set; }
    public IEffectManager EffectManager { get; private set; }
    public IDeathManager DeathManager { get; private set; }
    public IDamageHandler DamageHandler { get; private set; }
    public IParry Parry { get; private set; }
    public IClimb Climb { get; private set; }
    public IDeathLoad DeathLoad { get; private set; }

    public ITurningView TurningView { get; private set; }
    public IMovementView MovementView { get; private set; }
    public ICrouchView CrouchView { get; private set; }
    public IJumpView JumpView { get; private set; }
    public IRollView RollView { get; private set; }
    public IWeaponView WeaponView { get; private set; }
    public IParryView ParryView { get; private set; }
    public IClimbView ClimbView { get; private set; }
    public IStunVeiw StunVeiw { get; private set; }
    public IDeathView DeathView { get; private set; }

    public IStateMachine StateMachine { get; private set; }
    public IState Standing { get; private set; }
    public IState Crouching { get; private set; }
    public IState Jumping { get; private set; }
    public IState Rolling { get; private set; }
    public IState Attacking { get; private set; }
    public IState Parrying { get; private set; }
    public IState Climbing { get; private set; }
    public IState CastingAbility { get; private set; }
    public IState Interacting { get; private set; }
    public IState Stunned { get; private set; }
    public IState Dying { get; private set; }

    //Should be removed from here
    public IPlayerInterface PlayerInterface { get; private set; }

    private UIDocument doc;
    public UIDocument Document
    {
        get => doc;
        set
        {
            doc = value;
            PlayerInterface = new PlayerInterface(doc, HealthManager, EnergyManager);
        }
    }

    public void Initialize(PlayerInput unityPlayerInput)
    {
        PlayerInput = new InputSystemListener(unityPlayerInput);
        
        Collider = GetComponent<BoxCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Gravity = GetComponent<IGravity>();

        HealthManager = new HealthManager(healthManagerData);
        EnergyManager = new EnergyManager(energyManagerData);
        EffectManager = new EffectManager(effectManagerData);
        DeathManager = new DeathManager(HealthManager);
        WeaponModifierManager = new ModifierManager();
        AbilityModifierManager = new ModifierManager();
        DefenceModifierManager = new ModifierManager();
        DamageHandler = new DamageHandler(HealthManager, DefenceModifierManager, EffectManager);
        Turning = new Turning();
        Movement = new Movement(movementData, Rigidbody, Turning, EffectManager);
        Crouch = new Crouch(crouchData, Collider, EffectManager);
        Jump = new Jump(jumpData, Rigidbody, Gravity);
        Roll = new Roll(rollData, Collider, Rigidbody, Turning, DefenceModifierManager);
        Weapon = new CleaveMeleeWeapon(gameObject, weaponData, WeaponModifierManager, this, Turning);
        WeaponEnergyRegenerator = new EnergyRegenerator(energyRegeneratorData, EnergyManager, Weapon as IDamageDealer);
        Parry = new Parry(gameObject, parryData, Turning, this, DamageHandler, Weapon, DefenceModifierManager, WeaponModifierManager, EffectManager);
        Climb = new Climb(climbData, Rigidbody, Gravity, Turning);
        Interact = new Interact(gameObject, interactData);
        DeathLoad = new DeathLoad(DeathManager);

        AbilityManager = new AbilityManager();
        IAbility kanabo = new Kanabo(gameObject, kanaboData, AbilityManager, EnergyManager, AbilityModifierManager, Turning, this);
        IAbility daikyu = new Daikyu(gameObject, daikyuData, AbilityManager, EnergyManager, AbilityModifierManager, Turning, this);
        IAbility tessen = new Tessen(gameObject, tessenData, AbilityManager, EnergyManager, AbilityModifierManager, Turning, this, Collider);
        AbilityManager.AddAbility(eAbilityType.Kanabo, kanabo);
        AbilityManager.AddAbility(eAbilityType.Daikyu, daikyu);
        AbilityManager.AddAbility(eAbilityType.Tessen, tessen);

        GravityView = new GravityView(Gravity, Animator);
        TurningView = new TurningView(avatar, turningViewData, Turning);
        MovementView = new MovementView(Movement, Gravity, Animator, walkSound);
        CrouchView = new CrouchView(Animator);
        JumpView = new JumpView(Jump, Animator);
        RollView = new RollView(Roll, Animator);
        WeaponView = new PlayerWeaponView(weaponObject, weaponContainer, weaponGrip, Weapon, Animator, attackSound);
        ParryView = new ParryView(weaponObject, weaponContainer, weaponGrip, Parry, Animator);
        ClimbView = new ClimbView(Animator, Rigidbody);
        StunVeiw = new StunView(Animator);
        DeathView = new DeathView(Animator);

        CreateStateMachine();
    }

    private void CreateStateMachine()
    {
        StateMachine = new StateMachine();

        Standing = new StandingState(this, StateMachine, PlayerInput);
        Crouching = new CrouchingState(this, StateMachine, PlayerInput);
        Jumping = new JumpingState(this, StateMachine, PlayerInput);
        Rolling = new RollingState(this, StateMachine, PlayerInput);
        Attacking = new AttackingState(this, StateMachine, PlayerInput);
        Parrying = new ParryingState(this, StateMachine, PlayerInput);
        CastingAbility = new CastingAbilityState(this, StateMachine, PlayerInput);
        Interacting = new InteractingState(this, StateMachine, PlayerInput);
        Climbing = new ClimbingState(this, StateMachine, PlayerInput);
        Stunned = new StunnedState(this, StateMachine, PlayerInput);
        Dying = new DyingState(this, StateMachine, PlayerInput);

        StateMachine.Initialize(Standing);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SaveData(Data data)
    {
        data.playerHealth = HealthManager.Health.currentHealth;
        data.position = transform.position;
    }
}
