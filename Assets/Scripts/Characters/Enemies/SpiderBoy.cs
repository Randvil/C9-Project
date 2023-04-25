using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpiderBoy : MonoBehaviour, IEnemyBehavior, ITeam, IDamageable, IEffectable
{
    [SerializeField] private GameObject avatar;

    [SerializeField] private HealthManagerData healthManagerData;
    [SerializeField] private EffectManagerData effectManagerData;

    [SerializeField] private TurningViewData turningViewData;

    [SerializeField]
    private float SpawnGroupDelay;

    [SerializeField]
    private float SpawnSpiderDelay;

    [SerializeField]
    private int SpiderGroup;

    [SerializeField]
    private GameObject SpiderPrefab;

    [SerializeField]
    private float attackRadius;

    [SerializeField]
    private LayerMask enemyLayerMask;

    [SerializeField]
    private Slider healthBarSlider;

    private float positionX;
    private GameObject player;
    private bool playerInRadius = false;

    public UnityEvent deathEvent = new();

    public eTeam Team { get; private set; } = eTeam.Enemies;

    public UnityEvent<eDirection> DirectionalMoveEvent { get; } = new();
    public UnityEvent<eDirection> TurnEvent { get; } = new();
    public UnityEvent<Vector2> FreeMoveEvent { get; } = new();
    public UnityEvent StopEvent { get; } = new();
    public UnityEvent<Vector2> JumpEvent { get; } = new();
    public UnityEvent AttackEvent { get; } = new();
    public UnityEvent<eAbilityType> AbilityEvent { get; } = new();

    public UnityEvent SpawnEvent;

    public BoxCollider2D Collider { get; private set;}
    public Rigidbody2D Rigidbody { get; private set; }
    public ITurning Turning { get; private set; }
    public IHealthManager HealthManager { get; private set; }
    public IModifierManager DefenceModifierManager { get; private set; }
    public IEffectManager EffectManager { get; private set; }
    public IDamageHandler DamageHandler { get; private set; }
    public IDeathManager DeathManager { get; private set; }

    private HealthBarView healthBarView;
    private ITurningView turningView;

    private void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();

        Turning = new Turning();
        HealthManager = new HealthManager(healthManagerData);
        DefenceModifierManager = new ModifierManager();
        EffectManager = new EffectManager(effectManagerData);
        DamageHandler = new DamageHandler(HealthManager, DefenceModifierManager, EffectManager);
        DeathManager = new DeathManager(HealthManager);

        healthBarView = new(healthBarSlider, HealthManager, DeathManager);
        turningView = new TurningView(avatar, turningViewData, Turning);

        DamageHandler.TakeDamageEvent.AddListener(OnTakeDamage);
        DeathManager.DeathEvent.AddListener(OnDeath);
    }

    public void Update()
    {
        TurnToPlayer();
        Act();
    }
    public void Activate()
    {

    }

    public void Deactivate()
    {

    }

    public void SpawnSpiders()
    {
        SpawnEvent.Invoke();
        StartCoroutine(SpawnSpiderGroup());
        
    }

    public void Act()
    {
        if (playerInRadius == false) 
            CancelInvoke("SpawnSpiders");
        if (playerInRadius && !IsInvoking("SpawnSpiders"))
            InvokeRepeating("SpawnSpiders", 0.1f, SpawnGroupDelay);
    }

    public IEnumerator SpawnSpiderGroup()
    {
        positionX = transform.position.x;
        
        if (Turning.Direction == eDirection.Left) 
            positionX += 1;
        else positionX -= 1;

        for (int i = 0; i < SpiderGroup; i++)
        {
            yield return new WaitForSeconds(SpawnSpiderDelay);
            Instantiate(SpiderPrefab, new Vector3(positionX, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);
        }

    }
    private void TurnToPlayer()
    {
        if (PlayerPosX() != 0f)
        {
            if (transform.position.x > PlayerPosX() && Turning.Direction != eDirection.Left)
                ChangeDirection();
                
            if (transform.position.x < PlayerPosX() && Turning.Direction != eDirection.Right)
                ChangeDirection();
        }

        turningView.Turn();
    }

    private void ChangeDirection()
    {
        switch (Turning.Direction)
        {
            case eDirection.Right:
                Turning.Turn(eDirection.Left);
                TurnEvent.Invoke(eDirection.Left);
                break;

            case eDirection.Left:
                Turning.Turn(eDirection.Right);
                TurnEvent.Invoke(eDirection.Right);
                break;
        }
    }

    //also checks if player in attackRadius
    private float PlayerPosX()
    {
        Collider2D[] objectsNear = Physics2D.OverlapCircleAll(transform.position, attackRadius);

        if (objectsNear.Length == 0)
        {
            playerInRadius = false;
            return 0f;
        }
            
        foreach (Collider2D obj in objectsNear)
        {
            if (obj.TryGetComponent(out ITeam team) && team.Team != Team)
            {
                playerInRadius = true;
                return obj.transform.position.x;
            }
        }
        playerInRadius = false;
        return 0f;
    }

    private void OnTakeDamage(DamageInfo damageInfo)
    {
        if (player != null)
            return;

        Collider2D attacker = Physics2D.OverlapCircle(transform.position, attackRadius, enemyLayerMask);
        if (attacker != null)
            player = attacker.gameObject;
    }

    private void OnDeath()
    {
        deathEvent.Invoke();
        StartCoroutine(DestroyGameObjectCoroutine());
    }

    private IEnumerator DestroyGameObjectCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
