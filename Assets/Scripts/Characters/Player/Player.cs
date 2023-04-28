using UnityEngine.VFX;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, ITeamMember, IDamageable, IEffectable, IAbilityCaster, IDataSavable
{
    [SerializeField] private GameObject avatar;
    [SerializeField] private GameObject weaponObject;
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private Transform weaponGrip;
    [SerializeField] private AudioSource sharedAudioSource;
    [SerializeField] private AudioSource walkAudioSource;
    [SerializeField] private AudioSource takeDamageAudioSource;

    [SerializeField] private HealthManagerData healthManagerData;
    [SerializeField] private EnergyManagerData energyManagerData;
    [SerializeField] private EffectManagerData effectManagerData;
    [SerializeField] private InteractData interactData;
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
    [SerializeField] private DefensiveJumpData defensiveJumpData;
    [SerializeField] private RegenerationAbilityData regenerationAbilityData;

    [SerializeField] private TurningViewData turningViewData;
    [SerializeField] private JumpViewData jumpViewData;
    [SerializeField] private PlayerWeaponViewData playerWeaponViewData;

    [Header("Player Prefab VFX")]
    [SerializeField] private VisualEffect slashGraph;

    public Transform CameraFollowPoint => avatar.transform;

    public BoxCollider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public IPlayerInput PlayerInput { get; set; }
    public ITeam CharacterTeam { get; private set; }
    public IGravity Gravity { get; private set; }
    public GravityView GravityView { get; private set; }
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
    public IPlayerClimb Climb { get; private set; }

    public TurningView TurningView { get; private set; }
    public PlayerMovementView MovementView { get; private set; }
    public CrouchView CrouchView { get; private set; }
    public JumpView JumpView { get; private set; }
    public RollView RollView { get; private set; }
    public PlayerWeaponView WeaponView { get; private set; }
    public ParryView ParryView { get; private set; }
    public ClimbView ClimbView { get; private set; }
    public PlayerTakeDamageView TakeDamageView { get; private set; }
    public StunView StunView { get; private set; }
    public DeathView DeathView { get; private set; }

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

        CharacterTeam = new CharacterTeam(eTeam.Player);
        HealthManager = new HealthManager(healthManagerData);
        EnergyManager = new EnergyManager(energyManagerData);
        EffectManager = new EffectManager(this, effectManagerData);
        DeathManager = new DeathManager(HealthManager);
        WeaponModifierManager = new ModifierManager();
        AbilityModifierManager = new ModifierManager();
        DefenceModifierManager = new ModifierManager();
        DamageHandler = new DamageHandler(HealthManager, DefenceModifierManager, EffectManager, DeathManager);
        Turning = new Turning();
        Movement = new Movement(movementData, Rigidbody, Turning, EffectManager);
        Crouch = new Crouch(crouchData, Collider, EffectManager);
        Jump = new Jump(this, jumpData, Rigidbody, Gravity);
        Roll = new Roll(this, rollData, Collider, Rigidbody, Turning, DefenceModifierManager);
        Weapon = new CleaveMeleeWeapon(this, gameObject, weaponData, WeaponModifierManager, CharacterTeam, Turning);
        WeaponEnergyRegenerator = new EnergyRegenerator(energyRegeneratorData, EnergyManager, Weapon as IDamageDealer);
        Parry = new Parry(this, gameObject, parryData, Turning, CharacterTeam, DamageHandler, Weapon, DefenceModifierManager, WeaponModifierManager, EffectManager);
        Climb = new PlayerClimb(this, climbData, Rigidbody, Gravity, Turning);
        Interact = new Interact(this, gameObject, interactData);

        AbilityManager = new AbilityManager();
        IAbility kanabo = new Kanabo(this, gameObject, kanaboData, EnergyManager, AbilityModifierManager, Turning, CharacterTeam);
        IAbility daikyu = new Daikyu(this, gameObject, daikyuData, EnergyManager, AbilityModifierManager, Turning, CharacterTeam);
        IAbility tessen = new Tessen(this, gameObject, tessenData, EnergyManager, AbilityModifierManager, Turning, CharacterTeam, Collider);
        IAbility regeneration = new RegenerationAbility(this, regenerationAbilityData, EnergyManager, HealthManager);
        AbilityManager.AddAbility(eAbilityType.Kanabo, kanabo);
        AbilityManager.AddAbility(eAbilityType.Daikyu, daikyu);
        AbilityManager.AddAbility(eAbilityType.Tessen, tessen);
        AbilityManager.AddAbility(eAbilityType.Regeneration, regeneration);

        GravityView = new GravityView(Gravity, Animator);
        TurningView = new TurningView(this, avatar, turningViewData, Turning);
        MovementView = new PlayerMovementView(Movement, Gravity, Animator, walkAudioSource);
        CrouchView = new CrouchView(Crouch, Animator);
        JumpView = new JumpView(jumpViewData, Jump, Animator, sharedAudioSource);
        RollView = new RollView(Roll, Animator);
        WeaponView = new PlayerWeaponView(weaponObject, weaponContainer, weaponGrip, playerWeaponViewData, Weapon, Weapon as IDamageDealer, 
            Animator, sharedAudioSource, slashGraph);
        ParryView = new ParryView(weaponObject, weaponContainer, weaponGrip, Parry, Animator);
        ClimbView = new ClimbView(Climb, Animator);
        TakeDamageView = new PlayerTakeDamageView(DamageHandler, takeDamageAudioSource);
        StunView = new StunView(EffectManager, Animator);
        DeathView = new DeathView(DeathManager, Animator);

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
        data.playerEnergy = EnergyManager.Energy.currentEnergy;
        data.position = transform.position;

        foreach (KeyValuePair<int, IAbility> ability in AbilityManager.LearnedAbilities)
        {
            eAbilityType type = AbilityManager.Abilities.FirstOrDefault(x => x.Value == ability.Value).Key;
            AbilityPair abilityPair = new(ability.Key, type);
            if (data.learnedAbilities.Find(pair => pair.abilityType == abilityPair.abilityType) != null)
                data.learnedAbilities.Find(pair => pair.abilityType == abilityPair.abilityType).pos = abilityPair.pos;
            else 
                data.learnedAbilities.Add(abilityPair);
        }
    }
}
