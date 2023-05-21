using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Broodmother : BaseCreature, IBroodmotherBehavior
{
    [Header("Broodmother Prefab Components")]
    [SerializeField] protected Transform[] webSpawnPoints;
    [SerializeField] protected AudioSource movementAudioSource;

    [Header("Broodmother Data")]
    [SerializeField] protected SelectiveEffectManagerData SelectiveffectManagerData;
    [SerializeField] protected DamageHandlerWithShieldsData damageHandlerWithShieldsData;
    [SerializeField] protected HealthManagerData shieldManagerData;
    [SerializeField] protected EnergyManagerData energyManagerData;
    [SerializeField] protected MovementData movementData;
    [SerializeField] protected ClimbData climbData;
    [SerializeField] protected MeleeWeaponData meleeWeaponData;
    [SerializeField] protected KanaboData kanaboData;
    [SerializeField] protected OffensiveJumpData offensiveJumpData;
    [SerializeField] protected BroodmotherWebData broodmotherWebData;
    [SerializeField] protected RegenerationAbilityData regenerationAbilityData;
    [SerializeField] protected SwarmSpawningData swarmSpawningData;
    [SerializeField] protected StunEffectData selfStunData;
    [SerializeField] protected BroodmotherStrategyData broodmotherStrategyData;

    [SerializeField] protected NoArmsWeaponViewData weaponViewData;
    [SerializeField] protected CommonAbilityViewData stunAbilityViewData;
    [SerializeField] protected CommonAbilityViewData webAbilityViewData;
    [SerializeField] protected CommonAbilityViewData offensiveJumpAbilityViewData;

    [Header("Broodmother Visual Data")]
    [SerializeField] protected GameObject shield;

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

    private VisualElement document;
    public VisualElement Document
    {
        get => document;
        set
        {
            document = value;
            shieldBarView = new BroodmotherHealthBar(Document, ShieldManager, DeathManager, "shieldBar");
            HealthBarView = new BroodmotherHealthBar(Document, HealthManager, DeathManager, "broodmotherHealthBar");
        }
    }

    public BroodmotherStrategyData BroodmotherStrategyData => broodmotherStrategyData;

    protected IHealthBarView shieldBarView;
    protected MovementView movementView;
    protected NoArmsWeaponView weaponView;
    protected ClimbView climbView;
    protected StunView stunView;
    protected CommonAbilityView stunAbilityView;
    protected CommonAbilityView webAbilityView;
    protected CommonAbilityView offensiveJumpAbilityView;
    protected BroodmotherShieldView BroodmotherShieldView;

    protected override void Awake()
    {
        //Replace this with initialization in scene loader
        Initialize(GameObject.FindGameObjectWithTag("Player"));
    }

    protected void Initialize(GameObject enemy)
    {
        base.Awake();

        EffectManager = new SelectiveEffectManager(this, SelectiveffectManagerData);
        ShieldManager = new HealthManager(shieldManagerData);
        DamageHandler = new DamageHandlerWithShields(damageHandlerWithShieldsData, HealthManager, ShieldManager, DefenceModifierManager, EffectManager, DeathManager);
        EnergyManager = new EnergyManager(energyManagerData);
        Movement = new Movement(movementData, Rigidbody, Turning, EffectManager);
        Climb = new Climb(this, climbData, Rigidbody, Gravity, Turning);
        WeaponModifierManager = new ModifierManager();
        AbilityModifierManager = new ModifierManager();
        Weapon = new CleaveMeleeWeapon(this, gameObject, meleeWeaponData, WeaponModifierManager, CharacterTeam, Turning);
        StunAbility = new Kanabo(this, gameObject, kanaboData, EnergyManager, AbilityModifierManager, Turning, CharacterTeam);
        WebAbility = new BroodmotherWeb(this, gameObject, webSpawnPoints, broodmotherWebData, EnergyManager, AbilityModifierManager, Turning, CharacterTeam);
        OffensiveJumpAbility = new OffensiveJump(this, offensiveJumpData, EnergyManager, Rigidbody, Collider, Gravity, Turning, CharacterTeam, AbilityModifierManager);
        RegenerationAbility = new RegenerationAbility(this, regenerationAbilityData, EnergyManager, ShieldManager);
        SwarmSpawningAbility = new SwarmSpawning(this, swarmSpawningData, EnergyManager);

        CompoundAttack = new BroodmotherCompoundAttack(gameObject, Weapon, WebAbility, StunAbility, OffensiveJumpAbility, SwarmSpawningAbility);

        movementView = new MovementView(Movement, Animator, movementAudioSource);
        weaponView = new NoArmsWeaponView(weaponViewData, Weapon, Animator, sharedAudioSource);
        climbView = new ClimbView(Climb, Animator, sharedAudioSource);
        stunView = new StunView(EffectManager, Animator);
        stunAbilityView = new CommonAbilityView(stunAbilityViewData, StunAbility, Animator, sharedAudioSource);
        webAbilityView = new CommonAbilityView(webAbilityViewData, WebAbility, Animator, sharedAudioSource);
        offensiveJumpAbilityView = new CommonAbilityView(offensiveJumpAbilityViewData, OffensiveJumpAbility, Animator, sharedAudioSource);
        BroodmotherShieldView = new BroodmotherShieldView(shield, ShieldManager);

        ITalent selfStun = new BroodmotherSelfStun(selfStunData, EffectManager, ShieldManager);
        selfStun.Learn();

        currentBehavior = new BroodmotherStrategy(this, this, enemy);
        currentBehavior.Activate();

        DeathManager.DeathEvent.AddListener(OnDeath);
        DeathManager.DeathEvent.AddListener(GetComponent<EnemyVisualEffect>().ApplyDissolve);
        DeathManager.DeathEvent.AddListener(FindObjectOfType<BroodmotherDeathCheck>().ChangeDeathStatus);
        DamageHandler.TakeDamageEvent.AddListener(GetComponent<EnemyVisualEffect>().ApplyHurtEffect);
    }

    private void Start()
    {
        // Should be removed, use SceneLoader
        var a = FindObjectOfType<PanelManager>();
        Document = a.panels[0];
    }
}
