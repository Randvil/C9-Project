using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Broodmother : BaseCreature, IBroodmotherBehavior
{
    [Header("Broodmother Prefab Components")]
    [SerializeField] protected Transform[] webSpawnPoints;
    [SerializeField] protected Slider shieldBarSlider;
    [SerializeField] protected AudioSource movementAudioSource;
    [SerializeField] protected AudioSource sharedAudioSource;

    [Header("Broodmother Data")]
    [SerializeField] protected DamageHandlerWithShieldsData damageHandlerWithShieldsData;
    [SerializeField] protected HealthManagerData shieldManagerData;
    [SerializeField] protected EnergyManagerData energyManagerData;
    [SerializeField] protected MovementData movementData;
    [SerializeField] protected ClimbData climbData;
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected KanaboData kanaboData;
    [SerializeField] protected OffensiveJumpData offensiveJumpData;
    [SerializeField] protected BroodmotherWebData broodmotherWebData;
    [SerializeField] protected RegenerationAbilityData regenerationAbilityData;
    [SerializeField] protected SwarmSpawningData swarmSpawningData;
    [SerializeField] protected BroodmotherStrategyData broodmotherStrategyData;

    [SerializeField] protected NoArmsWeaponViewData weaponViewData;

    public IHealthManager ShieldManager { get; protected set; }
    public IMovement Movement { get; protected set; }
    public IClimb Climb { get; protected set; }
    public IEnergyManager EnergyManager { get; protected set; }
    public IModifierManager WeaponModifierManager { get; protected set; }
    public IModifierManager AbilityModifierManager { get; protected set; }
    public IWeapon Weapon { get; protected set; }
    public IDamageAbility StunAbility { get; protected set; }
    public IDamageAbility WebAbility { get; protected set; }
    public IAbility OffensiveJumpAbility { get; protected set; }
    public IAbility RegenerationAbility { get; protected set; }
    public IAbility SwarmSpawningAbility { get; protected set; }
    public ICompoundAttack CompoundAttack { get; protected set; }

    public BroodmotherStrategyData BroodmotherStrategyData => broodmotherStrategyData;

    protected IAIBehavior currentBehavior;

    protected NoArmsWeaponView weaponView;
    protected HealthBarView shieldBarView;

    protected override void Awake()
    {
        Initialize(GameObject.FindGameObjectWithTag("Player"));
    }

    protected void Initialize(GameObject enemy)
    {
        base.Awake();

        ShieldManager = new HealthManager(shieldManagerData);
        DamageHandler = new DamageHandlerWithShields(damageHandlerWithShieldsData, HealthManager, ShieldManager, DefenceModifierManager, EffectManager, DeathManager);
        EnergyManager = new EnergyManager(energyManagerData);
        Movement = new Movement(movementData, Rigidbody, Turning, EffectManager);
        Climb = new Climb(this, climbData, Rigidbody, Gravity, Turning);
        WeaponModifierManager = new ModifierManager();
        AbilityModifierManager = new ModifierManager();
        Weapon = new CleaveMeleeWeapon(this, gameObject, weaponData, WeaponModifierManager, CharacterTeam, Turning);
        StunAbility = new Kanabo(this, gameObject, kanaboData, EnergyManager, AbilityModifierManager, Turning, CharacterTeam);
        WebAbility = new BroodmotherWeb(this, gameObject, webSpawnPoints, broodmotherWebData, EnergyManager, AbilityModifierManager, Turning, CharacterTeam);
        OffensiveJumpAbility = new OffensiveJump(this, offensiveJumpData, EnergyManager, Rigidbody, Collider, Gravity, Turning, CharacterTeam, AbilityModifierManager);
        RegenerationAbility = new RegenerationAbility(this, regenerationAbilityData, EnergyManager, ShieldManager);
        SwarmSpawningAbility = new SwarmSpawning(this, swarmSpawningData, EnergyManager);

        CompoundAttack = new BroodmotherCompoundAttack(gameObject, Weapon, WebAbility, StunAbility, OffensiveJumpAbility, SwarmSpawningAbility);

        MovementView = new AnimationAndSoundMovementView(Movement, Animator, movementAudioSource);
        weaponView = new NoArmsWeaponView(weaponViewData, Weapon, Animator, sharedAudioSource);
        shieldBarView = new HealthBarView(shieldBarSlider, ShieldManager, DeathManager);
        
        currentBehavior = new BroodmotherStrategy(this, enemy);
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
