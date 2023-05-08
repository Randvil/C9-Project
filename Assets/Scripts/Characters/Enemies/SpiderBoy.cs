using UnityEngine;

public class SpiderBoy : BaseCreature, IWatchmanBehavior
{
    [Header("SpiderBoy Prefab Components")]
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform spiderSpawnPoint;
    [SerializeField] private AudioSource sharedAudioSource;

    [Header("SpiderBoy Data")]
    [SerializeField] private EnergyManagerData energyManagerData;
    [SerializeField] private RangedWeaponData bombThrowerData;
    [SerializeField] private NoArmsWeaponViewData weaponViewData;
    [SerializeField] private CreatureSpawnerData creatureSpawnerData;
    [SerializeField] private DefensiveJumpData defensiveJumpData;
    [SerializeField] private CompoundProtectionData compoundProtectionData;
    [SerializeField] private CreatureSpawnerViewData spiderSpawnerViewData;
    [SerializeField] private WatchmanStrategyData watchmanData;

    public WatchmanStrategyData WatchmanStrategyData => watchmanData;
    public IModifierManager WeaponModifierManager { get; private set; }
    public IWeapon Weapon { get; private set; }
    public IEnergyManager EnergyManager { get; private set; }
    public IAbility SpiderSpawnAbility { get; private set; }
    public IAbility JumpAbility { get; private set; }
    public ICompoundAttack CompoundAttack { get; private set; }
    public ICompoundProtection CompoundProtection { get; private set; }


    protected NoArmsWeaponView weaponView;
    protected CreatureSpawnerView spiderSpawnerView;

    protected IAIBehavior currentBehavior;

    protected override void Awake()
    {
        base.Awake();

        EnergyManager = new EnergyManager(energyManagerData);
        WeaponModifierManager = new ModifierManager();
        Weapon = new OrdinaryBow(this, gameObject, projectileSpawnPoint, bombThrowerData, WeaponModifierManager, CharacterTeam, Turning);
        SpiderSpawnAbility = new CreatureSpawner(this, spiderSpawnPoint, creatureSpawnerData, EnergyManager);
        JumpAbility = new DefensiveJump(this, defensiveJumpData, EnergyManager, Rigidbody, Gravity, Turning);
        CompoundAttack = new SpiderboyCompoundAttack(gameObject, Weapon, SpiderSpawnAbility);
        CompoundProtection = new SpiderboyCompoundProtection(compoundProtectionData, HealthManager, JumpAbility, EffectManager);

        weaponView = new NoArmsWeaponView(weaponViewData, Weapon, Animator, sharedAudioSource);
        spiderSpawnerView = new CreatureSpawnerView(spiderSpawnerViewData, SpiderSpawnAbility, sharedAudioSource);

        currentBehavior = new WatchmanStrategy(this);
        currentBehavior.Activate();

        DeathManager.DeathEvent.AddListener(OnDeath);
        DeathManager.DeathEvent.AddListener(GetComponent<EnemyVisualEffect>().ApplyDissolve);
        DamageHandler.TakeDamageEvent.AddListener(GetComponent<EnemyVisualEffect>().ApplyHurtEffect);
    }

    private void Update()
    {
        currentBehavior.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currentBehavior.PhysicsUpdate();
    }

    private void OnDeath()
    {
        currentBehavior.Deactivate();
        Destroy(gameObject, 1.2f);
    }
}
